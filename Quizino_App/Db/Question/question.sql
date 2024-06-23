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
	

-- INSERT INTO question (ques_text, ques_optiona, ques_optionb, ques_optionc, ques_optiond, ques_answer, ques_applicable_cat) 
-- VALUES ('A flashing red traffic light signifies that a driver should do what?', 'stop', 'speed up',
		-- 'proceed with caution', 'honk the horn', 0, '0');

-- INSERT INTO question (ques_text, ques_optiona, ques_optionb, ques_optionc, ques_optiond, ques_answer, ques_applicable_cat) 
-- VALUES ('According to Greek mythology, who was Apollos twin sister?', 'Aphrodite', 'Artemis',
		-- 'Venus', 'Athena', 1, '0');
		
-- INSERT INTO question (ques_text, ques_optiona, ques_optionb, ques_optionc, ques_optiond, ques_answer, ques_applicable_cat) 
-- VALUES ('According to the Bible, Moses and Aaron had a sister named what?', 'Jochebed', 'Artemis',
		-- 'Lead', 'Miriam', 3, '0');
		

-- INSERT INTO question (ques_text, ques_optiona, ques_optionb, ques_optionc, ques_optiond, ques_answer, ques_applicable_cat) 
-- VALUES ('What is the capital of India', 'Mumbai', 'New Delhi',
		-- 'Jaipur', 'Chennai', 1, '0');

-- INSERT INTO question (ques_text, ques_optiona, ques_optionb, ques_optionc, ques_optiond, ques_answer, ques_applicable_cat) 
-- VALUES ('Who was the first Indian to travel space', 'Rakesh Sharma', 'Rohit Sharma',
		-- 'B Kumarswamy', 'Abdul Kadir', 0, '0');