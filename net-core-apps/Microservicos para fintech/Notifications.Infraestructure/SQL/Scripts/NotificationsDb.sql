CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET utf8mb4;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `ApplicationUser` (
        `Id` int NOT NULL,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `Email` longtext CHARACTER SET utf8mb4 NULL,
        `Cellphone` longtext CHARACTER SET utf8mb4 NULL,
        `CreatedDate` datetime(6) NOT NULL,
        `UpdateDate` datetime(6) NOT NULL,
        `UserUpdate` varchar(10) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_ApplicationUser` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `Business` (
        `Id` int NOT NULL,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `Email` longtext CHARACTER SET utf8mb4 NULL,
        `Cellphone` longtext CHARACTER SET utf8mb4 NULL,
        `CreatedDate` datetime(6) NOT NULL,
        `UpdateDate` datetime(6) NOT NULL,
        `UserUpdate` varchar(10) CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `PK_Business` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `NotificationChannels` (
        `Id` int NOT NULL,
        `Name` varchar(200) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `PK_NotificationChannels` PRIMARY KEY (`Id`)
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `UserNotification` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `_userId` int NULL,
        `CreatedDate` datetime(6) NOT NULL,
        `UpdateDate` datetime(6) NOT NULL,
        `UserUpdate` varchar(10) CHARACTER SET utf8mb4 NULL,
        `Title` longtext CHARACTER SET utf8mb4 NULL,
        `Description` longtext CHARACTER SET utf8mb4 NULL,
        `Message` longtext CHARACTER SET utf8mb4 NULL,
        `Viewed` tinyint(1) NOT NULL,
        CONSTRAINT `PK_UserNotification` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UserNotification_ApplicationUser__userId` FOREIGN KEY (`_userId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `BusinessNotification` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `_businessId` int NULL,
        `CreatedDate` datetime(6) NOT NULL,
        `UpdateDate` datetime(6) NOT NULL,
        `UserUpdate` varchar(10) CHARACTER SET utf8mb4 NULL,
        `Title` longtext CHARACTER SET utf8mb4 NULL,
        `Description` longtext CHARACTER SET utf8mb4 NULL,
        `Message` longtext CHARACTER SET utf8mb4 NULL,
        `Viewed` tinyint(1) NOT NULL,
        CONSTRAINT `PK_BusinessNotification` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_BusinessNotification_Business__businessId` FOREIGN KEY (`_businessId`) REFERENCES `Business` (`Id`) ON DELETE RESTRICT
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `BusinessOwners` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `_userId` int NOT NULL,
        `_businessId` int NOT NULL,
        CONSTRAINT `PK_BusinessOwners` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_BusinessOwners_ApplicationUser__userId` FOREIGN KEY (`_userId`) REFERENCES `ApplicationUser` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_BusinessOwners_Business__businessId` FOREIGN KEY (`_businessId`) REFERENCES `Business` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `UserNotificationChannelRecords` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `_channelId` int NOT NULL,
        `CreatedDate` datetime(6) NOT NULL,
        `_notificationId` int NOT NULL,
        CONSTRAINT `PK_UserNotificationChannelRecords` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UserNotificationChannelRecords_NotificationChannels__channel~` FOREIGN KEY (`_channelId`) REFERENCES `NotificationChannels` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_UserNotificationChannelRecords_UserNotification__notificatio~` FOREIGN KEY (`_notificationId`) REFERENCES `UserNotification` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `UserNotificationRecipients` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `Email` longtext CHARACTER SET utf8mb4 NULL,
        `Cellphone` longtext CHARACTER SET utf8mb4 NULL,
        `_notificationId` int NOT NULL,
        CONSTRAINT `PK_UserNotificationRecipients` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_UserNotificationRecipients_UserNotification__notificationId` FOREIGN KEY (`_notificationId`) REFERENCES `UserNotification` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `BusinessNotificationChannelRecords` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `_channelId` int NOT NULL,
        `CreatedDate` datetime(6) NOT NULL,
        `_notificationId` int NOT NULL,
        CONSTRAINT `PK_BusinessNotificationChannelRecords` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_BusinessNotificationChannelRecords_BusinessNotification__not~` FOREIGN KEY (`_notificationId`) REFERENCES `BusinessNotification` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_BusinessNotificationChannelRecords_NotificationChannels__cha~` FOREIGN KEY (`_channelId`) REFERENCES `NotificationChannels` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE TABLE `BusinessNotificationRecipients` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Name` longtext CHARACTER SET utf8mb4 NULL,
        `Email` longtext CHARACTER SET utf8mb4 NULL,
        `Cellphone` longtext CHARACTER SET utf8mb4 NULL,
        `_notificationId` int NOT NULL,
        CONSTRAINT `PK_BusinessNotificationRecipients` PRIMARY KEY (`Id`),
        CONSTRAINT `FK_BusinessNotificationRecipients_BusinessNotification__notific~` FOREIGN KEY (`_notificationId`) REFERENCES `BusinessNotification` (`Id`) ON DELETE CASCADE
    ) CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    INSERT INTO `ApplicationUser` (`Id`, `Cellphone`, `CreatedDate`, `Email`, `Name`, `UpdateDate`, `UserUpdate`)
    VALUES (1, '', '2023-01-02 22:25:56', 'arthur.muller@capwise.com.br', 'Arthur Silva Muller', '2023-01-02 22:25:56', NULL);
    INSERT INTO `ApplicationUser` (`Id`, `Cellphone`, `CreatedDate`, `Email`, `Name`, `UpdateDate`, `UserUpdate`)
    VALUES (2, '', '2023-01-02 22:25:56', 'pablo@capwise.com.br', 'Pablo Maino', '2023-01-02 22:25:56', NULL);
    INSERT INTO `ApplicationUser` (`Id`, `Cellphone`, `CreatedDate`, `Email`, `Name`, `UpdateDate`, `UserUpdate`)
    VALUES (3, '', '2023-01-02 22:25:56', 'arthur@capwise.com.br', 'Arthur Decker', '2023-01-02 22:25:56', NULL);
    INSERT INTO `ApplicationUser` (`Id`, `Cellphone`, `CreatedDate`, `Email`, `Name`, `UpdateDate`, `UserUpdate`)
    VALUES (4, '', '2023-01-02 22:25:56', '', '', '2023-01-02 22:25:56', NULL);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    INSERT INTO `NotificationChannels` (`Id`, `Name`)
    VALUES (1, 'platform');
    INSERT INTO `NotificationChannels` (`Id`, `Name`)
    VALUES (2, 'push');
    INSERT INTO `NotificationChannels` (`Id`, `Name`)
    VALUES (3, 'email');
    INSERT INTO `NotificationChannels` (`Id`, `Name`)
    VALUES (4, 'sms');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_BusinessNotification__businessId` ON `BusinessNotification` (`_businessId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_BusinessNotificationChannelRecords__channelId` ON `BusinessNotificationChannelRecords` (`_channelId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_BusinessNotificationChannelRecords__notificationId` ON `BusinessNotificationChannelRecords` (`_notificationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_BusinessNotificationRecipients__notificationId` ON `BusinessNotificationRecipients` (`_notificationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_BusinessOwners__businessId` ON `BusinessOwners` (`_businessId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_BusinessOwners__userId` ON `BusinessOwners` (`_userId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_UserNotification__userId` ON `UserNotification` (`_userId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_UserNotificationChannelRecords__channelId` ON `UserNotificationChannelRecords` (`_channelId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_UserNotificationChannelRecords__notificationId` ON `UserNotificationChannelRecords` (`_notificationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    CREATE INDEX `IX_UserNotificationRecipients__notificationId` ON `UserNotificationRecipients` (`_notificationId`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `MigrationId` = '20230103012556_1') THEN

    INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
    VALUES ('20230103012556_1', '5.0.10');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

