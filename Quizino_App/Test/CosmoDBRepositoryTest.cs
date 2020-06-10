using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Test
{
    public class CosmoDBRepositoryTest
    {
        [Fact]
        public void TestDatabaseCreation()
        {
            var result = true;
            try
            {
                var repository = CosmoDBQuestionRepository.GetInstance();
                repository.DeleteDatabase();
            }
            catch (Exception)
            {
                result = false;
            }

            Assert.True(result);
        }
    }
}
