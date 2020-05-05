using Domain.Interfaces;
using System;
using System.Linq;

namespace QuestionBank
{
    public class QuestionViewModel : EntityBaseViewModel<IQuestion>
    {
        public QuestionViewModel(IQuestion entityBase) : base(entityBase)
        {
        }

        public string Text
        {
            get => EntityBase.Text;
            set => EntityBase.Text = value;
        }
        public string OptionA
        {
            get => EntityBase.OptionA;
            set => EntityBase.OptionA = value;
        }
        public string OptionB
        {
            get => EntityBase.OptionB;
            set => EntityBase.OptionB = value;
        }
        public string OptionC
        {
            get => EntityBase.OptionC;
            set => EntityBase.OptionC = value;
        }
        public string OptionD
        {
            get => EntityBase.OptionD;
            set => EntityBase.OptionD = value;
        }

        private string _applicableCategories;
        public string ApplicableCategories
        {
            get
            {
                if (EntityBase.ApplicableCategories != null && EntityBase.ApplicableCategories.Any())
                {
                    _applicableCategories = string.Join(',', EntityBase.ApplicableCategories);
                }

                return _applicableCategories;
            } 
            set
            {
                var enums = value.Split(',');
                foreach(var e in enums)
                {
                    Categories category;
                    if (Enum.TryParse<Categories>(e, out category))
                    {
                        if (!EntityBase.ApplicableCategories.Contains(category))
                        {
                            var list = EntityBase.ApplicableCategories.ToList();
                            list.Add(category);
                            EntityBase.ApplicableCategories = list;
                        }
                    }
                }
            }
        }

        public string Answer
        {
            get => EntityBase.Answer.ToString();
            set
            {
                var val = value.ToUpper();
                if(val == "A")
                {
                    EntityBase.Answer = AnswerOptions.A;
                }
                if (val == "B")
                {
                    EntityBase.Answer = AnswerOptions.B;
                }
                if (val == "C")
                {
                    EntityBase.Answer = AnswerOptions.C;
                }
                if (val == "D")
                {
                    EntityBase.Answer = AnswerOptions.D;
                }
            }
        }
    }
}
