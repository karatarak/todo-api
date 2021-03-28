INSERT INTO `todo`.`user` (`user_id`,`user_name`,`created_date`)
VALUES (UNHEX("3b146272cb4a45a79675591be5d09881"), "Peter", UTC_TIMESTAMP());

INSERT INTO `todo`.`board` (`board_id`,`owner_id`,`title`,`created_date`)
VALUES (UNHEX("c56b49411930403ab886e2388c567191"),UNHEX("3b146272cb4a45a79675591be5d09881"),"default",UTC_TIMESTAMP());

INSERT INTO `todo`.`item` (`item_id`,`owner_id`,`board_id`,`status`,`title`,`description`,`created_date`,`due_date`)
VALUES (
    UNHEX("0cf3daa6a9d54ed8802c694cf641e8aa"),
    UNHEX("3b146272cb4a45a79675591be5d09881"),
    UNHEX("c56b49411930403ab886e2388c567191"),
    "pending",
    "Feed the tiger",
    "Mr Snuggles is getting hungry",
    UTC_TIMESTAMP(),
    NULL);

INSERT INTO `todo`.`item` (`item_id`,`owner_id`,`board_id`,`status`,`title`,`description`,`created_date`,`due_date`)
VALUES (
    UNHEX("b409d4f1d69d493aacd32cbefbe6eeea"),
    UNHEX("3b146272cb4a45a79675591be5d09881"),
    UNHEX("c56b49411930403ab886e2388c567191"),
    "in_progress",
    "Training with Mr Miyagi", "Working towards my white belt",
    UTC_TIMESTAMP(),
    NULL);

INSERT INTO `todo`.`item` (`item_id`,`owner_id`,`board_id`,`status`,`title`,`description`,`created_date`,`due_date`)
VALUES (
    UNHEX("24b578af6f644a269fcd3def7922504b"),
    UNHEX("3b146272cb4a45a79675591be5d09881"),
    UNHEX("c56b49411930403ab886e2388c567191"),
    "complete",
    "Catch 20 Fish", "Level up my fishing skill",
    UTC_TIMESTAMP(),
    NULL);