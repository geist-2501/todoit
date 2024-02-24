BEGIN;
    CREATE TYPE priority_enum AS ENUM (
        'Low',
        'Medium',
        'High'
    );

    CREATE TABLE IF NOT EXISTS todos (
        id          UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        description VARCHAR(2000) NOT NULL,
        priority    priority_enum,
        done        bool DEFAULT false
    );
COMMIT;