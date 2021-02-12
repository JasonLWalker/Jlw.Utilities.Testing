using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jlw.Utilities.Testing
{
    public partial class BaseModelUtility<TModel>
    {
        public class MemberTest
        {
            private IEnumerable<object> _initData;
            private Dictionary<string, object> _expectedData = new Dictionary<string, object>();
            private Action<MemberTest> _fnTestCallback;
            private MemberSchema _schema;

            public object[] InitData => _initData.ToArray();
            public MemberSchema Schema => _schema;
            public Action<MemberTest> TestCallback => _fnTestCallback;

            public MemberTest(MemberSchema schema, IEnumerable<object> initData = null, IDictionary<string, object> expectedData=null, Action<MemberTest> fnTestCallback = null)
            {
                _schema = schema;
                _initData = initData;
                _fnTestCallback = fnTestCallback;
                if (expectedData != null)
                {
                    foreach (var expected in expectedData)
                    {
                        _expectedData.Add(expected.Key, expected.Value);
                    }
                }
            }

            public override string ToString()
            {
                if (_initData?.Count() == 1)
                {
                    return $"{GetAccessString(_schema.Access)} {_schema.Name}, {_initData.ToArray()[0]} ";
                }
                else
                {
                    return $"{GetAccessString(_schema.Access)} {_schema.Name}, {Newtonsoft.Json.JsonConvert.SerializeObject(_initData)}";
                }
            }
        }


    }
}
