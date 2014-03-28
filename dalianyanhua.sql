/*
MySQL Data Transfer
Source Host: localhost
Source Database: dalianyanhua
Target Host: localhost
Target Database: dalianyanhua
Date: 2014/3/29 1:12:59
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
-- Table structure for tj_suit
-- ----------------------------
CREATE TABLE `tj_suit` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` char(20) NOT NULL,
  `suitstring` char(100) NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for tj_xiangmu
-- ----------------------------
CREATE TABLE `tj_xiangmu` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` char(20) NOT NULL,
  `type` char(20) NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=gb2312;

-- ----------------------------
-- Table structure for tj_yuyue
-- ----------------------------
CREATE TABLE `tj_yuyue` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `peopleid` int(11) NOT NULL,
  `suitid` int(11) NOT NULL,
  `date` date NOT NULL,
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
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` char(20) NOT NULL DEFAULT '',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=gb2312;

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
INSERT INTO `tj_suit` VALUES ('3', '123', '1,2', '2014-03-29 00:36:50');
INSERT INTO `tj_xiangmu` VALUES ('1', '身高', '常规检查');
INSERT INTO `tj_xiangmu` VALUES ('2', '体重', '常规检查');
INSERT INTO `tj_xiangmu` VALUES ('3', '11', '22');
INSERT INTO `tj_yuyue` VALUES ('2', '1', '1', '2014-03-26');
INSERT INTO `yy_keshi` VALUES ('1', '内科');
INSERT INTO `yy_keshi` VALUES ('2', '外科');
