CREATE TABLE public.quiz_result (
    Id BIGINT PRIMARY KEY,
    Quiz_Id BIGINT NOT NULL,
    User_Id VARCHAR(256) NOT NULL,
    Score INT NOT NULL,
    Time_taken INT NOT NULL -- Assuming "Time_taken" is in minutes
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.quiz_result
    OWNER to bks;

COMMENT ON TABLE public.quiz_result
    IS 'Holds data about quiz result for users';