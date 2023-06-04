export interface Question {
    questionId: number;
    text: string;
    optionA: string;
    optionB: string;
    optionC: string;
    optionD: string;
    answer: number;
    caterogry: number;
    isNew: boolean;
    hasChanges:boolean;
  }
  
  export interface QuestionSearchResult {
    questions: Question[];
    TotalQuestions: number;
  }

  export class modelService{
    
  }
  