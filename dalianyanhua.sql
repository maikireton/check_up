/*
MySQL Data Transfer
Source Host: localhost
Source Database: dalianyanhua
Target Host: localhost
Target Database: dalianyanhua
Date: 2014/3/26 0:01:01
*/

SET FOREIGN_KEY_CHECKS=0;
-- ----------------------------
-- Table structure for tj_people
-- ----------------------------
CREATE TABLE `tj_people` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` char(12) NOT NULL,
  `sex` char(4) NOT NULL,
  `age` tinyint(4) NOT NULL,
  `tel` char(20) DEFAULT '',
  `corporation` char(60) DEFAULT '',
  `address` char(60) DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for tj_result
-- ----------------------------
CREATE TABLE `tj_result` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tj_people` int(11) NOT NULL,
  `tj_suit` int(11) NOT NULL,
  `tj_date` date NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for tj_result_neike
-- ----------------------------
CREATE TABLE `tj_result_neike` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `resultid` int(11) NOT NULL,
  `xueya` char(11) NOT NULL,
  `xinlv` char(11) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for tj_result_waike
-- ----------------------------
CREATE TABLE `tj_result_waike` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `suitid` int(11) NOT NULL,
  `height` int(4) NOT NULL,
  `weight` int(4) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for tj_suit
-- ----------------------------
CREATE TABLE `tj_suit` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` char(20) NOT NULL DEFAULT '',
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for tj_suit_neike
-- ----------------------------
CREATE TABLE `tj_suit_neike` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `suitid` int(11) NOT NULL,
  `xueya` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for tj_suit_waike
-- ----------------------------
CREATE TABLE `tj_suit_waike` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `suitid` int(11) NOT NULL,
  `height` tinyint(4) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for ys_people
-- ----------------------------
CREATE TABLE `ys_people` (
  `id` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for yy_keshi
-- ----------------------------
CREATE TABLE `yy_keshi` (
  `id` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Records 
-- ----------------------------
INSERT INTO `tj_people` VALUES ('1', '赵康', '男', '30', '18867878876', 'abc', '大连');
INSERT INTO `tj_people` VALUES ('2', 'a', '男', '12', '123123', 'sfdsf', 'sfdsf');
INSERT INTO `tj_people` VALUES ('3', 'sfsd', 's', '123', '13213', '1321', 'fdsf');
INSERT INTO `tj_people` VALUES ('4', 'a', '男', '12', '123123', 'sfdsf', 'sfdsf');
INSERT INTO `tj_people` VALUES ('5', 'a', '男', '12', '123123', 'sfdsf', 'sfdsf');
INSERT INTO `tj_people` VALUES ('6', 'a', '男', '12', '123123', 'sfdsf', 'sfdsf');
INSERT INTO `tj_people` VALUES ('7', 'a', '男', '12', '123123', 'sfdsf', 'sfdsf');
INSERT INTO `tj_people` VALUES ('8', 'a', '男', '12', '123123', 'sfdsf', 'sfdsf');
INSERT INTO `tj_people` VALUES ('9', 'a', '男', '12', '123123', 'sfdsf', 'sfdsf');
INSERT INTO `tj_result` VALUES ('1', '1', '0', '2014-03-23');
INSERT INTO `tj_result_neike` VALUES ('1', '0', '1', '1');
INSERT INTO `tj_suit` VALUES ('8', '123', '2014-03-24 21:32:45');
INSERT INTO `tj_suit_neike` VALUES ('2', '8', '1');
INSERT INTO `tj_suit_waike` VALUES ('2', '8', '1');
