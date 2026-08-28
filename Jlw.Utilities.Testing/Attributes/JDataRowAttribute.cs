using System;
using Jlw.Utilities.Data;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace Jlw.Utilities.Testing;

// Borrowed with permission from Jlw.DataUtilities GitHub dev repo, by Jason Walker


/// <summary>
/// Attribute to define in-line data for a test method.
/// Extends the DataRow attribute from MS test to allow automatic parsing of special JSON strings with custom tags
/// that will populate the values with random data in the specified formats
/// </summary>
[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
public class JDataRowAttribute : DataRowAttribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DataRowAttribute"/> class.
    /// </summary>
    public JDataRowAttribute() : this(Array.Empty<object>()) {}

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRowAttribute"/> class with an array of object arguments.
    /// </summary>
    /// <param name="data"> The data. </param>
    /// <remarks>This constructor is only kept for CLS compliant tests.</remarks>
    public JDataRowAttribute(object? data) : this([data]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRowAttribute"/> class with an array of string arguments.
    /// </summary>
    /// <param name="stringArrayData"> The string array data. </param>
    public JDataRowAttribute(string?[]? stringArrayData) : this([stringArrayData]) { }

    /// <summary>
    /// Initializes a new instance of the <see cref="DataRowAttribute"/> class with an array of object arguments.
    /// </summary>
    /// <param name="data"> The data. </param>
    public JDataRowAttribute(params object?[]? data) : base(data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            if (data[i] is string s)
            {
                try
                {
                    JToken jData = ParseConfigJson(s);
                    data[i] = jData.ToString(Formatting.None);
                }
                catch
                {
                    // Do nothing, string is not a valid JSON string
                    continue;
                }
            }
        }
    }

    
    
    public JToken ParseConfigJson(string jConfig)
    {
        var jObj = GenerateJArg(JToken.Parse(jConfig));

        return CopyJArgs(jObj);
    }

    public JToken CopyJArgs(JToken jObj)
    {
        if (jObj.HasValues)
        {
            if (jObj is JArray jArray)
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    CopyJArgs(jArray[i]);
                }
            }
            else if (jObj is JObject jObject)
            {
                foreach (var property in jObject.Properties())
                {
                    CopyJArgs(property.Value);
                }
            }
            else if (jObj is JValue jValue)
            {
                if (jValue.Value?.ToString()?.StartsWith("<copyfrom", StringComparison.InvariantCultureIgnoreCase) ?? false)
                {
                    var value = ParseCopyArgValue(jValue);
                    jObj.Replace((value is JToken) ? (JToken)value : new JValue(value));
                }
            }
        }
        else if (jObj is JValue jValue)
        {
            if (jValue.Value?.ToString()?.StartsWith("<copyfrom", StringComparison.InvariantCultureIgnoreCase) ?? false)
            {
                var value = ParseCopyArgValue(jValue);
                jObj.Replace((value is JToken) ? (JToken)value : new JValue(value));
            }
        }
        return jObj;

    }

    public JToken GenerateJArg(JToken jObj)
    {
        if (jObj.HasValues)
        {
            if (jObj is JArray jArray)
            {
                for (int i = 0; i < jArray.Count; i++)
                {
                    jArray[i] = GenerateJArg(jArray[i]);
                }
            }
            else if (jObj is JObject jObject)
            {
                foreach (var property in jObject.Properties())
                {
                    property.Value = GenerateJArg(property.Value);
                }
            }
            else if (jObj is JValue jValue)
            {
                var value = ParseArgValue(jValue.Value?.ToString() ?? "");
                if (value != null)
                {
                    jValue.Value = value;
                }
            }
        }
        else if (jObj is JValue jValue)
        {
            var value = ParseArgValue(jValue.Value?.ToString() ?? "");
            if (value != null)
            {
                jObj = new JValue(value);
            }
        }
        return jObj;
    }

    public object? ParseArgValue(string arg)
    {
        switch (arg)
        {
            case { } s when Regex.IsMatch(s, @"\<string[^\>]*\>"):
                return ParseStringValue(s);
            case { } s when Regex.Match(s, @"\<uint\s*\:?\s*(\d*)?,?(\d*)?\>", RegexOptions.IgnoreCase) is { Success: true } match:
                return Math.Abs(DataUtility.GenerateRandom<int>(DataUtility.ParseNullableInt(match.Groups[1].Value), DataUtility.ParseNullableInt(match.Groups[2].Value)));
            case { } s when Regex.Match(s, @"\<int\s*\:?\s*(\d*)?,?(\d*)?\>", RegexOptions.IgnoreCase) is { Success: true } match:
                return DataUtility.GenerateRandom<int>(DataUtility.ParseNullableInt(match.Groups[1].Value), DataUtility.ParseNullableInt(match.Groups[2].Value));

            case { } s when Regex.Match(s, @"\<ulong\s*\:?\s*(\d*)?,?(\d*)?\>", RegexOptions.IgnoreCase) is { Success: true } match:
                return Math.Abs(DataUtility.GenerateRandom<long>(DataUtility.ParseNullableInt(match.Groups[1].Value), DataUtility.ParseNullableInt(match.Groups[2].Value)));

            case { } s when Regex.Match(s, @"\<long\s*\:?\s*(\d*)?,?(\d*)?\>", RegexOptions.IgnoreCase) is { Success: true } match:
                return DataUtility.GenerateRandom<long>(DataUtility.ParseNullableInt(match.Groups[1].Value), DataUtility.ParseNullableInt(match.Groups[2].Value));

            case { } s when s.Equals("<bool>", StringComparison.InvariantCultureIgnoreCase):
                return DataUtility.GenerateRandom<bool>();

            case { } s when s.Equals("<null>", StringComparison.InvariantCultureIgnoreCase):
                return null;

            default:
                return arg;
        }
    }

    /// <summary>
    /// Implements the &lt;string&gt; meta tag to generate random string data.
    /// Usage is (values in square brackets are optional): 
    /// &lt;string:[minLength], [maxLength]:[transform]&gt;
    ///
    /// Examples:
    /// &lt;string&gt; - a random string with variable length
    /// &lt;string:10&gt; - a random string 10 characters long
    /// &lt;string:2,5&gt; - a random string that is a minimum of 2 and a maximum of 5 characters in length
    /// &lt;string:toUpper&gt; - a random uppercase string with variable length
    /// &lt;string:toLower&gt; - a random lowercase string with variable length
    /// &lt;string:10:toUpper&gt; - a random uppercase string 10 characters long
    /// ... etc.
    /// </summary>
    /// <param name="s"></param>
    /// <returns>random string value formated as specified if the &lt;string&gt; meta tag is found, otherwise the original data</returns>
    public string ParseStringValue(string s)
    {
        
        if (Regex.Match(s, @"\<string\s*\:?\s*(\d*)?,?(\d*)?\s*\:?\s*(toUpper|toLower)?\s*\>", RegexOptions.IgnoreCase) is { Success: true } match)
        {
            var val = DataUtility.GenerateRandom<string>(DataUtility.ParseNullableInt(match.Groups[1].Value), DataUtility.ParseNullableInt(match.Groups[2].Value));
            switch (match.Groups[3].Value.ToLower())
            {
                case "tolower":
                    return val.ToLower();
                case "toupper":
                    return val.ToUpper();
            }
            return val;
        }
        return s;
    }



    /// <summary>
    /// Implements the &lt;copyfrom&gt; meta tag to copy data from another JSON path.
    /// Usage is (values in square brackets are optional): 
    /// &lt;string:[JSON path]:[transform]&gt;
    ///
    /// Examples:
    /// &lt;copyfrom:Name&gt; - copies the value from the "Name" member of the root Json object
    /// &lt;copyfrom:Id:toUpper&gt; - copies the value from the "Id" member of the root Json object, and converts the value to uppercase
    /// &lt;copyfrom:Address:toLower&gt; - copies the value from the "Address" member of the root Json object, and converts the value to lowercase
    /// </summary>
    /// <param name="s"></param>
    /// <returns>random string value formated as specified if the &lt;copyfrom&gt; meta tag is found, otherwise the original data</returns>
    public object? ParseCopyArgValue(JValue jObj)
    {
        string arg = jObj.Value?.ToString() ?? "";

        switch (arg)
        {
            case { } s when Regex.Match(s, @"\<copyfrom\s*\:?\s*([\$\.\d\w-_]*)\s*\:?\s*(toUpper|toLower)?\s*\>", RegexOptions.IgnoreCase) is { Success: true } match:
            {
                var val = jObj.Root.SelectToken(match.Groups[1].Value);
                switch (match.Groups[2].Value.ToLower())
                {
                    case "tolower":
                        return val?.ToString().ToLower();
                    case "toupper":
                        return val?.ToString().ToUpper();
                }
                return val;
            }
        }

        return jObj.Value;
    }

}