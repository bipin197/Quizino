CREATE TABLE public.quiz
(
    quiz_id SERIAL PRIMARY KEY,
    Start TIMESTAMP NOT NULL,
    Finish TIMESTAMP NOT NULL,
    Category INT NOT NULL,
    Ques_Hash TEXT NOT NULL,
    Max_Time INT NOT NULL,
    Is_Active BOOLEAN DEFAULT FALSE,
    User_created VARCHAR(255) DEFAULT FALSE,
    Is_challenge BOOLEAN DEFAULT TRUE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.quiz
    OWNER to bks;

COMMENT ON TABLE public.quiz
    IS 'quiz table to hold quiz data';
	
CREATE TABLE public.quiz_result (
    Id SERIAL PRIMARY KEY,
    Quiz_Id BIGINT NOT NULL,
    User_Id VARCHAR(256) NOT NULL,
    Score INT NOT NULL,
    Time_taken INT NOT NULL
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.quiz_result
    OWNER to bks;

COMMENT ON TABLE public.quiz_result
    IS 'Holds data about quiz result for users';
	
-- Table: public.question

-- DROP TABLE IF EXISTS public.question;

CREATE TABLE IF NOT EXISTS public.question
(
    ques_id serial NOT NULL,
    ques_text text COLLATE pg_catalog.default NOT NULL,
    ques_optiona text COLLATE pg_catalog.default NOT NULL,
    ques_optionb text COLLATE pg_catalog.default NOT NULL,
    ques_optionc text COLLATE pg_catalog.default NOT NULL,
    ques_optiond text COLLATE pg_catalog.default NOT NULL,
    ques_answer integer NOT NULL,
    ques_applicable_cat text COLLATE pg_catalog.default NOT NULL,
    ques_hash text COLLATE pg_catalog.default NOT NULL,
    CONSTRAINT question_pkey PRIMARY KEY (ques_id)
)
TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.question
    OWNER TO bks;
