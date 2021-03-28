INSERT INTO users (user_id,user_name,created_date)
VALUES
(
    '3b146272cb4a45a79675591be5d09881',
    'Peter',
    now()
);

INSERT INTO boards (board_id,owner_id,title,created_date)
VALUES
(
    'c56b49411930403ab886e2388c567191',
    '3b146272cb4a45a79675591be5d09881',
    'default',
    now()
);

INSERT INTO items (item_id, owner_id, board_id, status, title, description, created_date, due_date)
VALUES
(
    '0cf3daa6a9d54ed8802c694cf641e8aa',
    '3b146272cb4a45a79675591be5d09881',
    'c56b49411930403ab886e2388c567191',
    'pending',
    'Feed the tiger',
    'Mr Snuggles is getting hungry',
    now(),
    null
);

INSERT INTO items (item_id, owner_id, board_id, status, title, description, created_date, due_date)
VALUES
(
    'b409d4f1d69d493aacd32cbefbe6eeea',
    '3b146272cb4a45a79675591be5d09881',
    'c56b49411930403ab886e2388c567191',
    'in_progress',
    'Training with Mr Miyagi',
    'Working towards my white belt',
    now(),
    NULL
);

INSERT INTO items (item_id, owner_id, board_id, status, title, description, created_date, due_date)
VALUES
(
    '24b578af6f644a269fcd3def7922504b',
    '3b146272cb4a45a79675591be5d09881',
    'c56b49411930403ab886e2388c567191',
    'complete',
    'Catch 20 Fish',
	'Level up my fishing skill',
    now(),
    NULL
);