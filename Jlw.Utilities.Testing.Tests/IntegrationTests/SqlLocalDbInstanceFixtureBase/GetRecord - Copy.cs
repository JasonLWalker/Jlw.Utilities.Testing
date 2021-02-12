using System;
using System.Data;
using Jlw.Utilities.Data;
using Jlw.Utilities.Data.DbUtility;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Jlw.Utilities.Testing.Tests.IntegrationTests.SqlLocalDbInstanceFixtureBase
{
    [TestClass]
    public class GetRecord : SqlLocalDbInstanceFixtureBase<ModularDataRepository<ITestDataModel, TestDataModel>>
    {
        protected string _sqlInitFilename = $"{AppDomain.CurrentDomain.BaseDirectory}Data\\Sql\\TSql\\InitializeDb.sql";
        /*
        protected RepositoryMethodDefinition<ITestDataModel, TestDataModel>.RecordCallback _recordCallback = (o) => new TestDataModel()
        {
            Description = DataUtility.ParseString(o, "Description"),
            Id = DataUtility.ParseLong(o, "Id"),
            Name = DataUtility.ParseString(o, "Name"),
            LastUpdated = DataUtility.ParseDateTime(o, "LastUpdated")
        };
        */
        
        [TestInitialize]
        public override void TestInitialize()
        {
            base.TestInitialize();
        }

        /*
        [TestMethod]
        public void Should_1()
        {
            // Arrange
            InitializeInstanceData(_sqlInitFilename);

            var expected = new TestDataModel() { Id = 1, Name = "Test User", Description = "This is a test user", LastUpdated = DateTime.Now };

            //DefaultRepo.AddNewDefinition("GetRecord", "sp_GetRecordData", new string[] { "Id" }, CommandType.StoredProcedure, RecordCallback);

            // Act
            var response = DefaultRepo.GetRecord(new TestDataModel() { Id = 1 });

            // Assert
            Assert.AreEqual(expected.Id, response.Id);
            Assert.AreEqual(expected.Name, response.Name);
            Assert.AreEqual(expected.Description, response.Description);

        }
        */
        
        /*
        [TestMethod]
        public void Should_2()
        {
            // Arrange
            InitializeInstanceData(_sqlInitFilename);
            DefaultRepo.AddNewDefinition("GetRecord", "sp_GetRecordData", new string[] { "Id" }, CommandType.StoredProcedure, _recordCallback);

            var expected = new TestDataModel(){ Id=1, Name="Test User", Description = "This is a test user", LastUpdated = DateTime.Now};

            //DefaultRepo.AddNewDefinition("GetRecord", "sp_GetRecordData", new string[] { "Id" }, CommandType.StoredProcedure, RecordCallback);

            // Act
            var response = DefaultRepo.GetRecord(new TestDataModel(){Id=1});

            // Assert
            Assert.AreEqual(expected.Id, response.Id);
            Assert.AreEqual(expected.Name, response.Name);
            Assert.AreEqual(expected.Description, response.Description);

        }
        */
    }
}
