CREATE TABLE public.quiz
(
    quiz_id bigint NOT NULL,
    quiz_desc text,
    quiz_start date NOT NULL,
    quiz_finish date NOT NULL,
	quiz_ques text NOT NULL,
    PRIMARY KEY (quiz_id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.quiz
    OWNER to bks;
	
CREATE SEQUENCE public.quiz_id_seq;

ALTER SEQUENCE public.quiz_id_seq
    OWNER TO bks;

COMMENT ON TABLE public.quiz
    IS 'quiz table to hold quiz data';