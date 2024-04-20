BEGIN;
    CREATE TABLE users (
        id                  UUID PRIMARY KEY DEFAULT gen_random_uuid(),
        email               VARCHAR(256) NOT NULL,
        password_hash       VARCHAR(500) NOT NULL,
        email_confirmed     BOOLEAN NOT NULL
    );
COMMIT;