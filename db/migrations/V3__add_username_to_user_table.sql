﻿BEGIN;
    ALTER TABLE users ADD username VARCHAR(256) NOT NULL DEFAULT '';
COMMIT;