using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;

using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing
{

    public abstract class BaseRepositoryFixture<TRepository, TTestInterface, TKey> : BaseRepositoryFixture where TRepository : class
    {
        protected static TRepository _repo;

        protected abstract TTestInterface GetNewTestData(TKey key, TTestInterface data);

        protected abstract TKey GetKeyValue(TTestInterface expected);

        protected abstract TTestInterface GetRecord(TTestInterface expected);

        protected abstract IEnumerable<TTestInterface> GetRecordList(TTestInterface expected);

        protected virtual TTestInterface SaveRecord(TTestInterface expected)
        {
            throw new NotImplementedException();
        }

        protected virtual TTestInterface InsertRecord(TTestInterface expected)
        {
            return SaveRecord(expected);
        }

        protected virtual TTestInterface UpdateRecord(TTestInterface expected)
        {
            return SaveRecord(expected);
        }

        protected abstract bool DeleteRecord(TTestInterface expected);



        public virtual void ShouldMatch_For_GetRecord(TTestInterface expected)
        {
            TTestInterface oResult = GetRecord(expected);
            AssertMembersAreEqual(expected, oResult);
        }

        public virtual void ShouldMatchResult_For_ListRecords(TTestInterface expected)
        {
            IEnumerable<TTestInterface> oResult = GetRecordList(expected);
            if (expected != null)
            {
                var actual = oResult.FirstOrDefault(o => GetKeyValue(o).Equals(GetKeyValue(expected)));
                AssertMembersAreEqual(expected, actual);
            }
        }

        public virtual void ShouldInsertData_ForInsertRecord(TTestInterface data)
        {
            if (data != null)
            {
                TTestInterface expected = GetNewTestData(default(TKey), data);
                TTestInterface oResult = InsertRecord(expected);
                AssertMembersAreEqual(expected, oResult, false, false);
            }
        }

        public virtual void ShouldInsertData_ForSaveRecord(TTestInterface data)
        {
            if (data != null)
            {
                TTestInterface expected = GetNewTestData(default(TKey), data);
                TTestInterface oResult = SaveRecord(expected);
                AssertMembersAreEqual(expected, oResult, false, false);
            }
        }

        public virtual void ShouldUpdateData_For_UpdateRecord(TTestInterface data)
        {
            if (data != null)
            {
                TTestInterface insert = GetNewTestData(default(TKey), data); //new Locations(0, data.LocationID + data.Name, data.LocationID + data.Description, data.LocationID + data.Address, data.LocationID + data.City, (data.LocationID + data.State).Substring(0,2), data.LocationID + data.Zip, data.LocationID + data.Latitude, data.LocationID + data.Longitude, data.LocationID + data.Image, !data.Inactive, "INSERT", data.ChangeBy, null);
                data = InsertRecord(insert);
                AssertMembersAreEqual(insert, data, false, false);

                TTestInterface expected = GetNewTestData(GetKeyValue(data), data);// new Locations(data.LocationID, data.LocationID + data.Name, data.LocationID + data.Description, data.LocationID + data.Address, data.LocationID + data.City, (data.LocationID + data.State).Substring(0,2), data.LocationID + data.Zip, data.LocationID + data.Latitude, data.LocationID + data.Longitude, data.LocationID + data.Image, !data.Inactive, "UPDATE", data.ChangeBy, null));
                TTestInterface oResult = UpdateRecord(expected);
                AssertMembersAreEqual(expected, oResult, true, false);
            }
        }

        public virtual void ShouldUpdateData_For_SaveRecord(TTestInterface data)
        {
            if (data != null)
            {
                TTestInterface insert = GetNewTestData(default(TKey), data); //new Locations(0, data.LocationID + data.Name, data.LocationID + data.Description, data.LocationID + data.Address, data.LocationID + data.City, (data.LocationID + data.State).Substring(0,2), data.LocationID + data.Zip, data.LocationID + data.Latitude, data.LocationID + data.Longitude, data.LocationID + data.Image, !data.Inactive, "INSERT", data.ChangeBy, null);
                data = SaveRecord(insert);
                AssertMembersAreEqual(insert, data, false, false);

                TTestInterface expected = GetNewTestData(GetKeyValue(data), data);// new Locations(data.LocationID, data.LocationID + data.Name, data.LocationID + data.Description, data.LocationID + data.Address, data.LocationID + data.City, (data.LocationID + data.State).Substring(0,2), data.LocationID + data.Zip, data.LocationID + data.Latitude, data.LocationID + data.Longitude, data.LocationID + data.Image, !data.Inactive, "UPDATE", data.ChangeBy, null));
                TTestInterface oResult = SaveRecord(expected);
                AssertMembersAreEqual(expected, oResult, true, false);
            }
        }

        public virtual void ShouldDeleteData_For_DeleteLocationRecord(TKey id)
        {
            TTestInterface data = GetNewTestData(id, default(TTestInterface));
            TTestInterface oResult = GetRecord(data);

            Assert.AreEqual(id, GetKeyValue(oResult));
            //Assert.AreEqual("Delete Test", oResult.Name);

            var actual = DeleteRecord(data);
            
            Assert.AreEqual(false, actual);
        }

        protected abstract void AssertMembersAreEqual(TTestInterface expected, TTestInterface actual, bool assertId = true, bool assertDate = true);
    }


    public class BaseRepositoryFixture
    {
        protected static readonly string sBasePath = Path.GetFullPath(Path.Combine(Assembly.GetEntryAssembly()?.Location ?? "", "../../../../../"));
        protected static string sDataDir = @"Data\";
        protected static string sDataPath = $"{sBasePath}{Assembly.GetCallingAssembly()?.GetName().Name}\\{sDataDir}";
        protected static string sSqlQueryDir = @"SQL Queries\";

        protected static IConfigurationBuilder _configBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);//.AddEnvironmentVariables();

        protected static IConfiguration _config = _configBuilder.Build();
        protected static string _testModule;

        protected static void ExecuteSqlScript(IDbCommand dbCommand, string type, string sqlObj)
        {
            string sqlScript = LoadSqlScript(type, sqlObj);
            var sqlCommands = sqlScript.Split(new []{"GO"}, StringSplitOptions.RemoveEmptyEntries);

            foreach (var sql in sqlCommands)
            {
                dbCommand.CommandText = sql;
                dbCommand.ExecuteNonQuery();
            }
        }

        protected static string LoadSqlScript(string type, string sqlObj)
        {
            if (type.Equals("init", StringComparison.InvariantCultureIgnoreCase))
                return File.ReadAllText($"{sDataPath}{_testModule}\\{sqlObj}.sql");

            return File.ReadAllText($"{sBasePath}{sSqlQueryDir}{type}\\{sqlObj}.sql");
        }

    }
}