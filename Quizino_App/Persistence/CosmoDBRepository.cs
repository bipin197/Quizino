using Persistence.DataTransferObjects;
using System.Collections.Generic;

namespace Persistence
{
    public class CosmoDBRepository
    {
        private static CosmoDBRepository _instance;
        private static List<QuestionDto> _repository;
        public static CosmoDBRepository GetInstance()
        {
            if (_instance == null)
            {
                _instance = new CosmoDBRepository();
                Initialize();
            }

            return _instance;
        }

        private static void Initialize()
        {
            _repository = new List<QuestionDto>();
        }
    }
}
