using Domain.Interfaces;

namespace QuestionBank
{
    public class EntityBaseViewModel<T> where T : IEntityBase
    {
        public T EntityBase { get; private set; }

        public EntityBaseViewModel(T entityBase)
        {
            EntityBase = entityBase;
        }
    }
}
