'----------------------------------------------------------------
' Copyright (C) 2006-2016 Josco Software Corporation
' All rights reserved.
'
' This source code is intended only as a supplement to Microsoft
' Development Tools and/or on-line documentation. See these other
' materials for detailed information regarding Microsoft code samples.
'
' THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY 
' OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT 
' LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR 
' FITNESS FOR A PARTICULAR PURPOSE.
'----------------------------------------------------------------
Option Strict On
Option Explicit On 

Imports System
Imports System.Data
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Data

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.Common.Data
    ' 类名    ：FlowData
    '
    ' 功能描述：
    '   　定义“公文_B_交接”、“公文_B_办理”、“公文_B_催办”
    '     “公文_B_督办”、“公文_B_操作日志”、“公文_B_附件”、
    '     “公文_B_相关文件”、“公文_B_相关文件附件”
    '     表相关的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("Code"), SerializableAttribute()> Public Class FlowData
        Inherits System.Data.DataSet

        '原交接号<=0时的特殊含义（通用）
        Public Const YJJH_YIBANTONGZHI As Integer = 0        '一般通知
        Public Const YJJH_ZHUANSONGQINGQIU As Integer = -1   '转送补阅请求
        Public Const YJJH_ZHUDONGBUYUE As Integer = -2       '主动补阅

        '“公文_B_交接”表信息定义
        '交接序号  ：文件处理流水号（每个文从1开始）
        '原交接号  ：<=0有特殊含义
        '            >  0：表示发送人最近一次接收的交接序号
        '            =  0：一般通知
        '            = -1：转送请求
        '            = -2：主动补阅
        '            = -3：会议通知
        '            = -4：转送会议通知
        '            = -5：调动通知
        '            = -6：转送调动通知
        '            = -7：借阅催还通知
        '发送序号  ：环节号
        '接收序号  ：环节中的顺序号
        '办理类型  ：收文、发文、报批件
        '办理子类  ：
        '            对于收文  ——签收、登记、拟办、阅批、承办、归档、中转
        '            对于发文  ——草拟、审核、会签、复核、签发、登记、缮印、用印、分发、归档、中转
        '            对于报批件——草拟、审核、会签、签批、归档、中转
        '办理状态  ：0-未接收、1-正在办理、2-办理完毕、3-文件被收回、4-文件被退回
        '交接标识  ：ABCDEFGH
        '           1-A = 0-文件从未发送
        '           1-A = 1-文件发送过
        '           2-B = 0-发送人不能看本交接单
        '           2-B = 1-发送人可看本交接单
        '           3-C = 0-接收人不能看本交接单
        '           3-C = 1-接收人可看本交接单
        '           4-D = 0-正常
        '           4-D = 1-退回
        '           5-E = 0-正常
        '           5-E = 1-收回
        '           6-F = 0-正常
        '           6-F = 1-通知
        '           7-G = 0-发送
        '           7-G = 1-回复
        '           8-H = 0-文件未办完
        '           8-H = 1-文件已办完
        '表名称
        Public Const TABLE_GW_B_JIAOJIE As String = "公文_B_交接"
        '字段序列
        Public Const FIELD_GW_B_JIAOJIE_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_JIAOJIE_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_JIAOJIE_YJJH As String = "原交接号"
        Public Const FIELD_GW_B_JIAOJIE_FSXH As String = "发送序号"
        Public Const FIELD_GW_B_JIAOJIE_FSR As String = "发送人"
        Public Const FIELD_GW_B_JIAOJIE_FSRQ As String = "发送日期"
        Public Const FIELD_GW_B_JIAOJIE_FSZZWJ As String = "发送纸质文件"
        Public Const FIELD_GW_B_JIAOJIE_FSDZWJ As String = "发送电子文件"
        Public Const FIELD_GW_B_JIAOJIE_FSZZFJ As String = "发送纸质附件"
        Public Const FIELD_GW_B_JIAOJIE_FSDZFJ As String = "发送电子附件"
        Public Const FIELD_GW_B_JIAOJIE_JSXH As String = "接收序号"
        Public Const FIELD_GW_B_JIAOJIE_JSR As String = "接收人"
        Public Const FIELD_GW_B_JIAOJIE_XB As String = "协办"
        Public Const FIELD_GW_B_JIAOJIE_JSRQ As String = "接收日期"
        Public Const FIELD_GW_B_JIAOJIE_JSZZWJ As String = "接收纸质文件"
        Public Const FIELD_GW_B_JIAOJIE_JSDZWJ As String = "接收电子文件"
        Public Const FIELD_GW_B_JIAOJIE_JSZZFJ As String = "接收纸质附件"
        Public Const FIELD_GW_B_JIAOJIE_JSDZFJ As String = "接收电子附件"
        Public Const FIELD_GW_B_JIAOJIE_BLZHQX As String = "办理最后期限"
        Public Const FIELD_GW_B_JIAOJIE_WCRQ As String = "完成日期"
        Public Const FIELD_GW_B_JIAOJIE_WTR As String = "委托人"
        Public Const FIELD_GW_B_JIAOJIE_BLLX As String = "办理类型"
        Public Const FIELD_GW_B_JIAOJIE_BLZL As String = "办理子类"
        Public Const FIELD_GW_B_JIAOJIE_BLZT As String = "办理状态"
        Public Const FIELD_GW_B_JIAOJIE_JJBS As String = "交接标识"
        Public Const FIELD_GW_B_JIAOJIE_SFDG As String = "是否读过"
        Public Const FIELD_GW_B_JIAOJIE_JJSM As String = "交接说明"
        Public Const FIELD_GW_B_JIAOJIE_BWTX As String = "备忘提醒"

        Public Const FIELD_GW_B_JIAOJIE_JJBZ As String = "交接备注"

        '约束错误信息

        '“公文_B_办理”表信息定义
        '是否批准；针对发文的签发、收文的阅批、报批件的签批。
        '是否批准标志位定义：
        '    发文、报批件
        '        第1位：0-无效  ，1-有效
        '        第2位：0-不同意，1-同意
        '        第3位：0-正常  ，1-保留意见
        '        第4位：未用
        '    收文
        '        第1位：0-无效  ，1-有效
        '        第2位：0-主批  ，1-不是主批
        '        第3位：0-圈阅  ，1-我的意见
        '        第4位：0-阅    , 1-批
        '    补阅
        '        第1  位：0-无效  ，1-有效
        '        第2-3位：10-同意，11-转送，00-不同意
        '        第4  位：未用
        '办理日期：填写意见的日期
        '填写日期：填写办理结果的日期
        '备    注：公文_B_交接表中的办理子类有可能与公文_B_办理表中的办理子类记录得不一致
        '表名称
        Public Const TABLE_GW_B_BANLI As String = "公文_B_办理"
        '字段序列
        Public Const FIELD_GW_B_BANLI_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_BANLI_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_BANLI_BLR As String = "办理人"
        Public Const FIELD_GW_B_BANLI_BLLX As String = "办理类型"
        Public Const FIELD_GW_B_BANLI_BLZL As String = "办理子类"

        Public Const FIELD_GW_B_BANLI_XSXH As String = "显示序号"

        Public Const FIELD_GW_B_BANLI_BLRQ As String = "办理日期"
        Public Const FIELD_GW_B_BANLI_SFPZ As String = "是否批准"
        Public Const FIELD_GW_B_BANLI_BLYJ As String = "办理意见"
        Public Const FIELD_GW_B_BANLI_BJNR As String = "便笺内容"
        Public Const FIELD_GW_B_BANLI_DLR As String = "代理人"
        Public Const FIELD_GW_B_BANLI_DLRQ As String = "代理日期"
        Public Const FIELD_GW_B_BANLI_BLJG As String = "办理结果"
        Public Const FIELD_GW_B_BANLI_TXRQ As String = "填写日期"
        Public Const FIELD_GW_B_BANLI_XZYDRY As String = "限制阅读人员"
        '约束错误信息

        '“公文_B_催办”表信息定义
        '表名称
        Public Const TABLE_GW_B_CUIBAN As String = "公文_B_催办"
        '字段序列
        Public Const FIELD_GW_B_CUIBAN_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_CUIBAN_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_CUIBAN_CBXH As String = "催办序号"
        Public Const FIELD_GW_B_CUIBAN_CBR As String = "催办人"
        Public Const FIELD_GW_B_CUIBAN_CBRQ As String = "催办日期"
        Public Const FIELD_GW_B_CUIBAN_BCBR As String = "被催办人"
        Public Const FIELD_GW_B_CUIBAN_CBSM As String = "催办说明"
        '约束错误信息

        '“公文_B_督办”表信息定义
        '表名称
        Public Const TABLE_GW_B_DUBAN As String = "公文_B_督办"
        '字段序列
        Public Const FIELD_GW_B_DUBAN_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_DUBAN_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_DUBAN_DBXH As String = "督办序号"
        Public Const FIELD_GW_B_DUBAN_DBR As String = "督办人"
        Public Const FIELD_GW_B_DUBAN_DBRQ As String = "督办日期"
        Public Const FIELD_GW_B_DUBAN_BDBR As String = "被督办人"
        Public Const FIELD_GW_B_DUBAN_DBYQ As String = "督办要求"
        Public Const FIELD_GW_B_DUBAN_DBJG As String = "督办结果"
        '约束错误信息

        '“公文_B_操作日志”表信息定义
        '表名称
        Public Const TABLE_GW_B_CAOZUORIZHI As String = "公文_B_操作日志"
        '字段序列
        Public Const FIELD_GW_B_CAOZUORIZHI_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_CAOZUORIZHI_CZXH As String = "操作序号"
        Public Const FIELD_GW_B_CAOZUORIZHI_CZR As String = "操作人"
        Public Const FIELD_GW_B_CAOZUORIZHI_CZSJ As String = "操作时间"
        Public Const FIELD_GW_B_CAOZUORIZHI_CZSM As String = "操作说明"
        '约束错误信息

        Public Enum enumFileDownloadStatus
            NotDownload = 0 '没有下载
            HasDownload = 1 '已经下载
        End Enum
        '“公文_B_附件”表信息定义
        '表名称
        Public Const TABLE_GW_B_FUJIAN As String = "公文_B_附件"
        '字段序列
        Public Const FIELD_GW_B_FUJIAN_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_FUJIAN_WJXH As String = "序号"
        Public Const FIELD_GW_B_FUJIAN_WJSM As String = "说明"
        Public Const FIELD_GW_B_FUJIAN_WJYS As String = "页数"
        Public Const FIELD_GW_B_FUJIAN_WJWZ As String = "位置"        '服务器文件位置(相对于FTP根的路径)
        '附加信息(显示/编辑时用)
        Public Const FIELD_GW_B_FUJIAN_XSXH As String = "显示序号"
        Public Const FIELD_GW_B_FUJIAN_BDWJ As String = "本地文件"    '下载后的文件位置(完整路径)
        Public Const FIELD_GW_B_FUJIAN_XZBZ As String = "下载标志"    '是否下载?
        '约束错误信息

        '“公文_B_相关文件”表信息定义
        '表名称
        Public Const TABLE_GW_B_XIANGGUANWENJIAN As String = "公文_B_相关文件"
        '字段序列
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_WJXH As String = "序号"
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_NBXH As String = "顺序号"
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_DQWJBS As String = "当前文件标识"
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_DCWJBS As String = "顶层文件标识"
        Public Const FIELD_GW_B_XIANGGUANWENJIAN_SJWJBS As String = "上级文件标识"
        '约束错误信息

        '“公文_B_相关文件附件”表信息定义
        '表名称
        Public Const TABLE_GW_B_XIANGGUANWENJIANFUJIAN As String = "公文_B_相关文件附件"
        '字段序列
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH As String = "序号"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM As String = "说明"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS As String = "页数"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ As String = "位置"
        '附加信息(显示/编辑时用)
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XSXH As String = "显示序号"
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ As String = "本地文件"  '下载后的文件位置
        Public Const FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XZBZ As String = "下载标志"  '是否下载?
        '约束错误信息

        '“公文_B_督办_交接”表信息定义(虚拟)
        '表名称
        Public Const TABLE_GW_B_DUBAN_JIAOJIE As String = "公文_B_督办_交接"
        '字段序列
        Public Const FIELD_GW_B_DUBAN_JIAOJIE_BCJG As String = "本次结果"
        '约束错误信息

        '“公文_B_催办_交接”表信息定义(虚拟)
        '表名称
        Public Const TABLE_GW_B_CUIBAN_JIAOJIE As String = "公文_B_催办_交接"
        '字段序列
        '约束错误信息

        '“公文_B_审批意见”表信息定义(虚拟)
        '表名称
        Public Const TABLE_GW_B_SHENPIYIJIAN As String = "公文_B_审批意见"
        '字段序列
        Public Const FIELD_GW_B_SHENPIYIJIAN_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_SHENPIYIJIAN_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLLX As String = "办理类型"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLZL As String = "办理子类"
        Public Const FIELD_GW_B_SHENPIYIJIAN_JSR As String = "接收人"
        Public Const FIELD_GW_B_SHENPIYIJIAN_XB As String = "协办"
        Public Const FIELD_GW_B_SHENPIYIJIAN_SFTY As String = "是否同意"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLRQ As String = "办理日期"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLYJ As String = "办理意见"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BJNR As String = "便笺内容"
        Public Const FIELD_GW_B_SHENPIYIJIAN_DLR As String = "代理人"
        Public Const FIELD_GW_B_SHENPIYIJIAN_DLRQ As String = "代理日期"
        Public Const FIELD_GW_B_SHENPIYIJIAN_BLJG As String = "办理结果"
        Public Const FIELD_GW_B_SHENPIYIJIAN_TXRQ As String = "填写日期"
        Public Const FIELD_GW_B_SHENPIYIJIAN_RYXH As String = "人员序号"
        Public Const FIELD_GW_B_SHENPIYIJIAN_XZJB As String = "行政级别"
        Public Const FIELD_GW_B_SHENPIYIJIAN_ZZDM As String = "组织代码"

        Public Const FIELD_GW_B_SHENPIYIJIAN_XSXH As String = "显示序号"

        '约束错误信息

        '“公文_V_全部审批文件”表信息定义(视图)
        '表名称
        Public Const TABLE_GW_V_SHENPIWENJIAN_NEW As String = "公文_V_全部审批文件新"
        '字段序列
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJBS As String = "文件标识"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJLX As String = "文件类型"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_BLLX As String = "办理类型"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJZL As String = "文件子类"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_ZSDW As String = "主送单位"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJBT As String = "文件标题"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJZH As String = "文件字号"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_JGDZ As String = "机关代字"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJNF As String = "文件年份"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJXH As String = "文件序号"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_MMDJ As String = "秘密等级"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_JJCD As String = "紧急程度"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_WJND As String = "文件年度"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_ZBDW As String = "主办单位"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_NGR As String = "拟稿人"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_NGRQ As String = "拟稿日期"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_BLZT As String = "办理状态"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_LSH As String = "流水号"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_ZTC As String = "主题词"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_KSSW As String = "快速收文"

        '2008-08-12
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_QFR As String = "签发人"
        Public Const FIELD_GW_V_SHENPIWENJIAN_NEW_QFRQ As String = "签发日期"
        '约束错误信息

        '文件的相关文件类别
        Public Enum enumXGWJLB
            FlowFile = 0    '指向系统内工作流文件
            FujianFile = 1  '指向附件指定的物理文件
        End Enum

        '“公文_V_审批文件_附件”表信息定义(虚拟)
        '表名称
        Public Const TABLE_GW_B_SHENPIWENJIAN_FUJIAN As String = "公文_V_审批文件_附件"
        '字段序列
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS As String = "类别标识"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJLX As String = "文件类型"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BLLX As String = "办理类型"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJZL As String = "文件子类"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBT As String = "文件标题"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZSDW As String = "主送单位"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_JGDZ As String = "机关代字"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJNF As String = "文件年份"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJXH As String = "文件序号"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJND As String = "文件年度"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZBDW As String = "主办单位"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_NGR As String = "拟稿人"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_NGRQ As String = "拟稿日期"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BLZT As String = "办理状态"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LSH As String = "流水号"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZTC As String = "主题词"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_KSSW As String = "快速收文"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH As String = "序号"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJYS As String = "页数"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ As String = "位置"
        '附加信息(显示/编辑时用)
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH As String = "显示序号"
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ As String = "本地文件"  '下载后的文件位置
        Public Const FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XZBZ As String = "下载标志"  '是否下载?
        '约束错误信息

        '“公文_B_文件发送虚拟表”表信息定义
        '表名称
        Public Const TABLE_GW_B_VT_WENJIANFASONG As String = "公文_B_文件发送虚拟表"
        '字段序列
        Public Const FIELD_GW_B_VT_WENJIANFASONG_JSR As String = "接收人"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_BLSY As String = "办理事宜"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_BLQX As String = "办理期限"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FSR As String = "发送人"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FSRQ As String = "发送日期"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_WJZT As String = "文件载体"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_WJZZFS As String = "纸质文件份数"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_WJDZFS As String = "电子文件份数"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FJZT As String = "附件载体"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FJZZFS As String = "纸质附件份数"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_FJDZFS As String = "电子附件份数"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_SYJB As String = "事宜级别"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_XB As String = "协办"
        Public Const FIELD_GW_B_VT_WENJIANFASONG_WTR As String = "委托人"
        '约束错误信息

        '“公文_B_文件接收虚拟表”表信息定义
        '表名称
        Public Const TABLE_GW_B_VT_WENJIANJIESHOU As String = "公文_B_文件接收虚拟表"
        '字段序列
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSR As String = "发送人"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSRQ As String = "发送日期"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_BLSY As String = "交办事宜"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSRQ As String = "接收日期"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSWJZZFS As String = "送来纸质文件份数"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSWJDZFS As String = "送来电子文件份数"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSFJZZFS As String = "送来纸质附件份数"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSFJDZFS As String = "送来电子附件份数"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSWJZZFS As String = "接收纸质文件份数"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSWJDZFS As String = "接收电子文件份数"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSFJZZFS As String = "接收纸质附件份数"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JSFJDZFS As String = "接收电子附件份数"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSXH As String = "发送序号"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_YJJH As String = "原交接号"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSRBLSY As String = "发送人办理事宜"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_JJBS As String = "交接标识"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_XB As String = "协办"
        Public Const FIELD_GW_B_VT_WENJIANJIESHOU_FSRXB As String = "发送人协办"
        '约束错误信息

        '“公文_B_文件收回虚拟表”表信息定义
        '表名称
        Public Const TABLE_GW_B_VT_WENJIANSHOUHUI As String = "公文_B_文件收回虚拟表"
        '字段序列
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSR As String = "接收人"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_BLSY As String = "交办事宜"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSRQ As String = "发送日期"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSWJZZFS As String = "发送纸质文件份数"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSWJDZFS As String = "发送电子文件份数"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSFJZZFS As String = "发送纸质附件份数"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSFJDZFS As String = "发送电子附件份数"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSRQ As String = "接收日期"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSWJZZFS As String = "接收纸质文件份数"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSWJDZFS As String = "接收电子文件份数"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSFJZZFS As String = "接收纸质附件份数"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JSFJDZFS As String = "接收电子附件份数"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSXH As String = "发送序号"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_YJJH As String = "原交接号"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_JJBS As String = "交接标识"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSR As String = "发送人"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_XB As String = "协办"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_SFDG As String = "是否读过"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSRBLSY As String = "发送人办理事宜"
        Public Const FIELD_GW_B_VT_WENJIANSHOUHUI_FSRXB As String = "发送人协办"
        '约束错误信息

        '“公文_B_文件退回虚拟表”表信息定义
        '表名称
        Public Const TABLE_GW_B_VT_WENJIANTUIHUI As String = "公文_B_文件退回虚拟表"
        '字段序列
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSR As String = "发送人"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSRQ As String = "发送日期"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_BLSY As String = "交办事宜"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSRQ As String = "接收日期"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSWJZZFS As String = "送来纸质文件份数"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSWJDZFS As String = "送来电子文件份数"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSFJZZFS As String = "送来纸质附件份数"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSFJDZFS As String = "送来电子附件份数"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSWJZZFS As String = "接收纸质文件份数"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSWJDZFS As String = "接收电子文件份数"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSFJZZFS As String = "接收纸质附件份数"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JSFJDZFS As String = "接收电子附件份数"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSXH As String = "发送序号"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_YJJH As String = "原交接号"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSRBLSY As String = "发送人办理事宜"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_JJBS As String = "交接标识"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_XB As String = "协办"
        Public Const FIELD_GW_B_VT_WENJIANTUIHUI_FSRXB As String = "发送人协办"
        '约束错误信息

        '“公文_B_文件补阅虚拟表”表信息定义
        '表名称
        Public Const TABLE_GW_B_VT_WENJIANBUYUE As String = "公文_B_文件补阅虚拟表"
        '字段序列
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_YJJH As String = "原交接号"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSXH As String = "发送序号"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSR As String = "发送人"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSRQ As String = "发送日期"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSZZWJ As String = "发送纸质文件"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSDZWJ As String = "发送电子文件"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSZZFJ As String = "发送纸质附件"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_FSDZFJ As String = "发送电子附件"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSXH As String = "接收序号"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSR As String = "接收人"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_XB As String = "协办"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSRQ As String = "接收日期"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSZZWJ As String = "接收纸质文件"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSDZWJ As String = "接收电子文件"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSZZFJ As String = "接收纸质附件"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JSDZFJ As String = "接收电子附件"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLZHQX As String = "办理最后期限"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_WCRQ As String = "完成日期"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_WTR As String = "委托人"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLLX As String = "办理类型"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLZL As String = "办理子类"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLZT As String = "办理状态"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JJBS As String = "交接标识"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_SFDG As String = "是否读过"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_JJSM As String = "交接说明"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BWTX As String = "备忘提醒"
        Public Const FIELD_GW_B_VT_WENJIANBUYUE_BLQK As String = "办理情况"
        '约束错误信息

        '“公文_B_VT_承办情况虚拟表”表信息定义
        '办理日期：填写办理结果的日期
        '备    注：公文_B_交接表中的办理子类有可能与公文_B_承办表中的办理子类记录得不一致.
        '          公文_B_承办表中的办理子类是具体操作类别
        '表名称
        Public Const TABLE_GW_B_VT_CHENGBANQINGKUANG As String = "公文_B_VT_承办情况虚拟表"
        '字段序列
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_WJBS As String = "文件标识"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_JJXH As String = "交接序号"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLXH As String = "办理序号"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLLX As String = "办理类型"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLZL As String = "办理子类"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLRQ As String = "办理日期"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLJG As String = "办理结果"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_BLRY As String = "办理人"
        Public Const FIELD_GW_B_VT_CHENGBANQINGKUANG_XBBZ As String = "协办"
        '约束错误信息

        '“公文_V_全部公文新”表信息定义(视图)
        '表名称
        Public Const TABLE_GW_V_QUANBUGONGWEN As String = "公文_V_全部公文新"
        '字段序列
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJBS As String = "文件标识"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJLX As String = "文件类型"
        Public Const FIELD_GW_V_QUANBUGONGWEN_BLLX As String = "办理类型"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJZL As String = "文件子类"
        Public Const FIELD_GW_V_QUANBUGONGWEN_ZSDW As String = "主送单位"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJBT As String = "文件标题"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJZH As String = "文件字号"
        Public Const FIELD_GW_V_QUANBUGONGWEN_JGDZ As String = "机关代字"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJNF As String = "文件年份"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJXH As String = "文件序号"
        Public Const FIELD_GW_V_QUANBUGONGWEN_MMDJ As String = "秘密等级"
        Public Const FIELD_GW_V_QUANBUGONGWEN_JJCD As String = "紧急程度"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJND As String = "文件年度"
        Public Const FIELD_GW_V_QUANBUGONGWEN_ZBDW As String = "主办单位"
        Public Const FIELD_GW_V_QUANBUGONGWEN_NGR As String = "拟稿人"
        Public Const FIELD_GW_V_QUANBUGONGWEN_NGRQ As String = "拟稿日期"
        Public Const FIELD_GW_V_QUANBUGONGWEN_BLZT As String = "办理状态"
        Public Const FIELD_GW_V_QUANBUGONGWEN_LSH As String = "流水号"
        Public Const FIELD_GW_V_QUANBUGONGWEN_ZTC As String = "主题词"
        Public Const FIELD_GW_V_QUANBUGONGWEN_KSSW As String = "快速收文"
        Public Const FIELD_GW_V_QUANBUGONGWEN_WJRQ As String = "文件日期"
        Public Const FIELD_GW_V_QUANBUGONGWEN_FSRQ As String = "发送日期"
        Public Const FIELD_GW_V_QUANBUGONGWEN_BWTX As String = "备忘提醒"


        '“公文_V_全部督查工作”表信息定义(视图)
        '表名称
        Public Const TABLE_GW_V_DUCHAGONGZUO As String = "公文_V_全部督查工作"
        '字段序列
        Public Const FIELD_GW_V_DUCHAGONGZUO_WJBS As String = "文件标识"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXBS As String = "立项标识"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BLBS As String = "办理标识"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BJBS As String = "办结标识"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LSH As String = "流水号"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BLLX As String = "办理类型"
        Public Const FIELD_GW_V_DUCHAGONGZUO_WJZL As String = "文件子类"
        Public Const FIELD_GW_V_DUCHAGONGZUO_RWLB As String = "任务类别"
        Public Const FIELD_GW_V_DUCHAGONGZUO_SCJD As String = "所处阶段"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BLZT As String = "办理状态"
        Public Const FIELD_GW_V_DUCHAGONGZUO_MMDJ As String = "秘密等级"
        Public Const FIELD_GW_V_DUCHAGONGZUO_JJCD As String = "紧急程度"
        Public Const FIELD_GW_V_DUCHAGONGZUO_XMBT As String = "项目标题"
        Public Const FIELD_GW_V_DUCHAGONGZUO_DCBH As String = "督查编号"
        Public Const FIELD_GW_V_DUCHAGONGZUO_DCWH As String = "督查文号"
        Public Const FIELD_GW_V_DUCHAGONGZUO_DCLX As String = "督查类型"

        Public Const FIELD_GW_V_DUCHAGONGZUO_DCR As String = "督查人"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BLSX As String = "办理时限"
        Public Const FIELD_GW_V_DUCHAGONGZUO_CBDW As String = "承办单位"
        Public Const FIELD_GW_V_DUCHAGONGZUO_CBR As String = "承办人"
        Public Const FIELD_GW_V_DUCHAGONGZUO_XBDW As String = "协办单位"
        Public Const FIELD_GW_V_DUCHAGONGZUO_XBR As String = "协办人"

        Public Const FIELD_GW_V_DUCHAGONGZUO_LXPZR As String = "立项批准人"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXPZRQ As String = "立项批准日期"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BJPZR As String = "办结批准人"
        Public Const FIELD_GW_V_DUCHAGONGZUO_BJPZRQ As String = "办结批准日期"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXDW As String = "立项单位"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXR As String = "立项人"
        Public Const FIELD_GW_V_DUCHAGONGZUO_LXRQ As String = "立项日期"

        '新的工作流，督查单
        Public Const FIELD_GW_V_DUCHAGONGZUO_PZR As String = "批准人"
        Public Const FIELD_GW_V_DUCHAGONGZUO_PZRQ As String = "批准日期"

        '显示字段
        Public Const FIELD_GW_V_DUCHAGONGZUO_BWTX As String = "备忘提醒"


        '“公文_V_移交文件”虚拟表信息定义
        '表名称
        Public Const TABLE_GW_V_YIJIAOWENJIAN As String = "公文_V_移交文件"
        '字段序列
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJBS As String = "文件标识"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_YJRY As String = "移交人"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_YJRQ As String = "移交日期"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_YJSM As String = "移交说明"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_JSRY As String = "接收人"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_JSRQ As String = "接收日期"
        '显示字段
        Public Const FIELD_GW_V_YIJIAOWENJIAN_SFYJ As String = "是否移交"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_SFJS As String = "是否接收"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJLX As String = "文件类型"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_BLLX As String = "办理类型"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJZL As String = "文件子类"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_ZSDW As String = "主送单位"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJBT As String = "文件标题"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJZH As String = "文件字号"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_JGDZ As String = "机关代字"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJNF As String = "文件年份"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJXH As String = "文件序号"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_MMDJ As String = "秘密等级"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_JJCD As String = "紧急程度"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_WJND As String = "文件年度"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_ZBDW As String = "主办单位"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_NGR As String = "拟稿人"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_NGRQ As String = "拟稿日期"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_BLZT As String = "办理状态"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_LSH As String = "流水号"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_ZTC As String = "主题词"
        Public Const FIELD_GW_V_YIJIAOWENJIAN_KSSW As String = "快速收文"






        '定义初始化表类型enum
        Public Enum enumTableType
            '***********************************
            GW_B_JIAOJIE = 1
            GW_B_BANLI = 2
            GW_B_CUIBAN = 3
            GW_B_DUBAN = 4
            GW_B_CAOZUORIZHI = 5
            GW_B_FUJIAN = 6
            GW_B_XIANGGUANWENJIAN = 7
            GW_B_XIANGGUANWENJIANFUJIAN = 8
            '***********************************
            GW_B_CUIBAN_JIAOJIE = 9
            GW_B_DUBAN_JIAOJIE = 10
            '***********************************
            GW_B_SHENPIYIJIAN = 11
            GW_B_SHENPIWENJIAN_FUJIAN = 12
            '***********************************
            GW_V_SHENPIWENJIAN_NEW = 13
            '***********************************
            GW_B_VT_WENJIANFASONG = 14
            '***********************************
            GW_B_VT_WENJIANJIESHOU = 15
            '***********************************
            GW_B_VT_WENJIANSHOUHUI = 16
            '***********************************
            GW_B_VT_WENJIANTUIHUI = 17
            '***********************************
            GW_B_VT_WENJIANBUYUE = 18
            '***********************************
            GW_B_VT_CHENGBANQINGKUANG = 19
            '***********************************
            GW_V_QUANBUGONGWEN = 20
            '***********************************
            GW_V_DUCHAGONGZUO = 21
            '***********************************
            GW_V_YIJIAOWENJIAN = 22

        End Enum

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Private Sub New(ByVal info As SerializationInfo, ByVal context As StreamingContext)
            MyBase.New(info, context)
        End Sub

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New(ByVal objenumTableType As enumTableType)
            MyBase.New()
            Try
                Dim objDataTable As System.Data.DataTable
                Dim strErrMsg As String
                objDataTable = Me.createDataTables(strErrMsg, objenumTableType)
                If Not (objDataTable Is Nothing) Then
                    Me.Tables.Add(objDataTable)
                End If
            Catch ex As Exception
            End Try

        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.FlowData)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        '将给定DataTable加入到DataSet中
        '----------------------------------------------------------------
        Public Function appendDataTable(ByVal table As System.Data.DataTable) As String

            Dim strErrMsg As String = ""

            Try
                Me.Tables.Add(table)
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            appendDataTable = strErrMsg

        End Function

        '----------------------------------------------------------------
        '根据指定类型创建dataTable
        '----------------------------------------------------------------
        Public Function createDataTables( _
            ByRef strErrMsg As String, _
            ByVal enumType As enumTableType) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Select Case enumType
                Case enumTableType.GW_B_JIAOJIE
                    table = createDataTables_Jiaojie(strErrMsg)
                Case enumTableType.GW_B_BANLI
                    table = createDataTables_Banli(strErrMsg)
                Case enumTableType.GW_B_CUIBAN
                    table = createDataTables_Cuiban(strErrMsg)
                Case enumTableType.GW_B_DUBAN
                    table = createDataTables_Duban(strErrMsg)
                Case enumTableType.GW_B_CAOZUORIZHI
                    table = createDataTables_Caozuorizhi(strErrMsg)
                Case enumTableType.GW_B_FUJIAN
                    table = createDataTables_Fujian(strErrMsg)
                Case enumTableType.GW_B_XIANGGUANWENJIAN
                    table = createDataTables_Xiangguanwenjian(strErrMsg)
                Case enumTableType.GW_B_XIANGGUANWENJIANFUJIAN
                    table = createDataTables_XiangguanwenjianFujian(strErrMsg)

                Case enumTableType.GW_B_CUIBAN_JIAOJIE
                    table = createDataTables_Cuiban_Jiaojie(strErrMsg)
                Case enumTableType.GW_B_DUBAN_JIAOJIE
                    table = createDataTables_Duban_Jiaojie(strErrMsg)

                Case enumTableType.GW_B_SHENPIYIJIAN
                    table = createDataTables_Shenpiyijian(strErrMsg)
                Case enumTableType.GW_V_SHENPIWENJIAN_NEW
                    table = createDataTables_Shenpiwenjian(strErrMsg)
                Case enumTableType.GW_B_SHENPIWENJIAN_FUJIAN
                    table = createDataTables_Shenpiwenjian_Fujian(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANFASONG
                    table = createDataTables_VT_Wenjianfasong(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANJIESHOU
                    table = createDataTables_VT_Wenjianjieshou(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANSHOUHUI
                    table = createDataTables_VT_Wenjianshouhui(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANTUIHUI
                    table = createDataTables_VT_Wenjiantuihui(strErrMsg)

                Case enumTableType.GW_B_VT_WENJIANBUYUE
                    table = createDataTables_Buyue(strErrMsg)

                Case enumTableType.GW_B_VT_CHENGBANQINGKUANG
                    table = createDataTables_VT_Chengbanqingkuang(strErrMsg)

                Case enumTableType.GW_V_QUANBUGONGWEN
                    table = createDataTables_QuanbuGongwen(strErrMsg)


                Case enumTableType.GW_V_DUCHAGONGZUO
                    table = createDataTables_Duchagongzuo(strErrMsg)



                Case enumTableType.GW_V_YIJIAOWENJIAN
                    table = createDataTables_YijiaoWenjian(strErrMsg)



                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_JIAOJIE
        '----------------------------------------------------------------
        Private Function createDataTables_Jiaojie(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_JIAOJIE)
                With table.Columns
                    .Add(FIELD_GW_B_JIAOJIE_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_YJJH, GetType(System.Int32))

                    .Add(FIELD_GW_B_JIAOJIE_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_JIAOJIE_FSZZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_FSDZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_FSZZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_FSDZFJ, GetType(System.Int32))

                    .Add(FIELD_GW_B_JIAOJIE_JSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_XB, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_JIAOJIE_JSZZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JSDZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JSZZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JSDZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_BLZHQX, GetType(System.DateTime))
                    .Add(FIELD_GW_B_JIAOJIE_WCRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_B_JIAOJIE_WTR, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLZT, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_SFDG, GetType(System.Int32))
                    .Add(FIELD_GW_B_JIAOJIE_JJSM, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BWTX, GetType(System.Int32))

                    .Add(FIELD_GW_B_JIAOJIE_JJBZ, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Jiaojie = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_BANLI
        '----------------------------------------------------------------
        Private Function createDataTables_Banli(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_BANLI)
                With table.Columns
                    .Add(FIELD_GW_B_BANLI_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_JJXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_BANLI_BLR, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_BLZL, GetType(System.String))

                    .Add(FIELD_GW_B_BANLI_XSXH, GetType(System.Int32))


                    .Add(FIELD_GW_B_BANLI_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_BANLI_SFPZ, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_BLYJ, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_BJNR, GetType(System.String))

                    .Add(FIELD_GW_B_BANLI_DLR, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_DLRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_B_BANLI_BLJG, GetType(System.String))
                    .Add(FIELD_GW_B_BANLI_TXRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_B_BANLI_XZYDRY, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Banli = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_CUIBAN
        '----------------------------------------------------------------
        Private Function createDataTables_Cuiban(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_CUIBAN)
                With table.Columns
                    .Add(FIELD_GW_B_CUIBAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_CUIBAN_CBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_CUIBAN_CBR, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_CBRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_CUIBAN_CBSM, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_BCBR, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Cuiban = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_DUBAN
        '----------------------------------------------------------------
        Private Function createDataTables_Duban(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_DUBAN)
                With table.Columns
                    .Add(FIELD_GW_B_DUBAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_DUBAN_DBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_DUBAN_DBR, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_DBRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_DUBAN_DBYQ, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_BDBR, GetType(System.String))

                    .Add(FIELD_GW_B_DUBAN_DBJG, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Duban = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_CAOZUORIZHI
        '----------------------------------------------------------------
        Private Function createDataTables_Caozuorizhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_CAOZUORIZHI)
                With table.Columns
                    .Add(FIELD_GW_B_CAOZUORIZHI_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_CAOZUORIZHI_CZXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_CAOZUORIZHI_CZR, GetType(System.String))
                    .Add(FIELD_GW_B_CAOZUORIZHI_CZSJ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_CAOZUORIZHI_CZSM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Caozuorizhi = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_FUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Fujian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_FUJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_FUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_FUJIAN_WJXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_FUJIAN_WJSM, GetType(System.String))
                    .Add(FIELD_GW_B_FUJIAN_WJYS, GetType(System.Int32))
                    .Add(FIELD_GW_B_FUJIAN_WJWZ, GetType(System.String))

                    .Add(FIELD_GW_B_FUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_FUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_GW_B_FUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Fujian = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_XIANGGUANWENJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Xiangguanwenjian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_XIANGGUANWENJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_WJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_NBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_DQWJBS, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_DCWJBS, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIAN_SJWJBS, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Xiangguanwenjian = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_XIANGGUANWENJIANFUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_XiangguanwenjianFujian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_XIANGGUANWENJIANFUJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJSM, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJYS, GetType(System.Int32))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_WJWZ, GetType(System.String))

                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_GW_B_XIANGGUANWENJIANFUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_XiangguanwenjianFujian = table

        End Function



        '----------------------------------------------------------------
        '创建TABLE_GW_B_CUIBAN_JIAOJIE
        '----------------------------------------------------------------
        Private Function createDataTables_Cuiban_Jiaojie(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_CUIBAN_JIAOJIE)
                With table.Columns
                    .Add(FIELD_GW_B_CUIBAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_CUIBAN_CBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_CUIBAN_CBR, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_CBRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_CUIBAN_CBSM, GetType(System.String))
                    .Add(FIELD_GW_B_CUIBAN_BCBR, GetType(System.String))

                    '交接表信息
                    .Add(FIELD_GW_B_JIAOJIE_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLZT, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Cuiban_Jiaojie = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_DUBAN_JIAOJIE
        '----------------------------------------------------------------
        Private Function createDataTables_Duban_Jiaojie(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_DUBAN_JIAOJIE)
                With table.Columns
                    .Add(FIELD_GW_B_DUBAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_DUBAN_DBXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_DUBAN_DBR, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_DBRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_DUBAN_DBYQ, GetType(System.String))
                    .Add(FIELD_GW_B_DUBAN_BDBR, GetType(System.String))

                    .Add(FIELD_GW_B_DUBAN_DBJG, GetType(System.String))

                    '交接表信息
                    .Add(FIELD_GW_B_JIAOJIE_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_JIAOJIE_BLZT, GetType(System.String))

                    .Add(FIELD_GW_B_DUBAN_JIAOJIE_BCJG, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Duban_Jiaojie = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_SHENPIYIJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Shenpiyijian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_SHENPIYIJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_SHENPIYIJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_XB, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_SFTY, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLYJ, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BJNR, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_DLR, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_DLRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_BLJG, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_TXRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_RYXH, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_XZJB, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIYIJIAN_ZZDM, GetType(System.String))

                    .Add(FIELD_GW_B_SHENPIYIJIAN_XSXH, GetType(System.Int32))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shenpiyijian = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_V_SHENPIWENJIAN_NEW
        '----------------------------------------------------------------
        Private Function createDataTables_Shenpiwenjian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_V_SHENPIWENJIAN_NEW)
                With table.Columns
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJBS, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_BLLX, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJZL, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_ZSDW, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJBT, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJZH, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_JGDZ, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJNF, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJXH, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_MMDJ, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_JJCD, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_WJND, GetType(System.Int32))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_ZBDW, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_NGR, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_NGRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_BLZT, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_LSH, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_ZTC, GetType(System.String))
                    .Add(FIELD_GW_V_SHENPIWENJIAN_NEW_KSSW, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shenpiwenjian = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_SHENPIWENJIAN_FUJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_Shenpiwenjian_Fujian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_SHENPIWENJIAN_FUJIAN)
                With table.Columns
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LBBS, GetType(System.Int32))

                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJLX, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJZL, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZSDW, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJBT, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_JGDZ, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJNF, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJXH, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_WJND, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZBDW, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_NGR, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_NGRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BLZT, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_LSH, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_ZTC, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_KSSW, GetType(System.Int32))

                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJYS, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_FJWZ, GetType(System.String))

                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_BDWJ, GetType(System.String))
                    .Add(FIELD_GW_B_SHENPIWENJIAN_FUJIAN_XZBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shenpiwenjian_Fujian = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_VT_WENJIANFASONG
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Wenjianfasong(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANFASONG)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_BLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_BLQX, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_WJZT, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_WJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_WJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FJZT, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_FJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_SYJB, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANFASONG_WTR, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Wenjianfasong = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_VT_WENJIANJIESHOU
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Wenjianjieshou(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANJIESHOU)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_BLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_YJJH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSRBLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANJIESHOU_FSRXB, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Wenjianjieshou = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_VT_WENJIANSHOUHUI
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Wenjianshouhui(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANSHOUHUI)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_BLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_YJJH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_SFDG, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSRBLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANSHOUHUI_FSRXB, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Wenjianshouhui = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_VT_WENJIANTUIHUI
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Wenjiantuihui(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANTUIHUI)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_BLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSWJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSWJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSFJZZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JSFJDZFS, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_YJJH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSRBLSY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANTUIHUI_FSRXB, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Wenjiantuihui = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_VT_WENJIANBUYUE
        '----------------------------------------------------------------
        Private Function createDataTables_Buyue(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_WENJIANBUYUE)
                With table.Columns
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_YJJH, GetType(System.Int32))

                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSZZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSDZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSZZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_FSDZFJ, GetType(System.Int32))

                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_XB, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSZZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSDZWJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSZZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JSDZFJ, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLZHQX, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_WCRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_WTR, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLZT, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_SFDG, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_JJSM, GetType(System.String))
                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BWTX, GetType(System.Int32))

                    .Add(FIELD_GW_B_VT_WENJIANBUYUE_BLQK, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Buyue = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_B_VT_CHENGBANQINGKUANG
        '----------------------------------------------------------------
        Private Function createDataTables_VT_Chengbanqingkuang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_B_VT_CHENGBANQINGKUANG)
                With table.Columns
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_WJBS, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_JJXH, GetType(System.Int32))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLXH, GetType(System.Int32))

                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLLX, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLZL, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLJG, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_BLRY, GetType(System.String))
                    .Add(FIELD_GW_B_VT_CHENGBANQINGKUANG_XBBZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_VT_Chengbanqingkuang = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_V_QUANBUGONGWEN
        '----------------------------------------------------------------
        Private Function createDataTables_QuanbuGongwen(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_V_QUANBUGONGWEN)
                With table.Columns
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_BLLX, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJZL, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_ZSDW, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJBT, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJZH, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_JGDZ, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJNF, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJXH, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_MMDJ, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_JJCD, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJND, GetType(System.Int32))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_ZBDW, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_NGR, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_NGRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_BLZT, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_LSH, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_ZTC, GetType(System.String))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_KSSW, GetType(System.Int32))
                    .Add(FIELD_GW_V_QUANBUGONGWEN_WJRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_V_QUANBUGONGWEN_FSRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_V_QUANBUGONGWEN_BWTX, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_QuanbuGongwen = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GW_V_DUCHAGONGZUO
        '----------------------------------------------------------------
        Private Function createDataTables_Duchagongzuo(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_V_DUCHAGONGZUO)
                With table.Columns
                    .Add(FIELD_GW_V_DUCHAGONGZUO_WJBS, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXBS, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BLBS, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BJBS, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LSH, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BLLX, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_WJZL, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_RWLB, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_SCJD, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_MMDJ, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_JJCD, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BLZT, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_XMBT, GetType(System.String))

                    .Add(FIELD_GW_V_DUCHAGONGZUO_DCBH, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_DCWH, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_DCLX, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_DCR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BLSX, GetType(System.DateTime))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_CBDW, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_CBR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_XBDW, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_XBR, GetType(System.String))

                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXPZR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXPZRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BJPZR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_BJPZRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXDW, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_LXRQ, GetType(System.DateTime))

                    .Add(FIELD_GW_V_DUCHAGONGZUO_BWTX, GetType(System.String))

                    '新的工作流，督查单
                    .Add(FIELD_GW_V_DUCHAGONGZUO_PZR, GetType(System.String))
                    .Add(FIELD_GW_V_DUCHAGONGZUO_PZRQ, GetType(System.DateTime))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Duchagongzuo = table

        End Function


        '----------------------------------------------------------------
        '创建TABLE_GW_V_YIJIAOWENJIAN
        '----------------------------------------------------------------
        Private Function createDataTables_YijiaoWenjian(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GW_V_YIJIAOWENJIAN)
                With table.Columns
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJBS, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_YJRY, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_YJRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_YJSM, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_JSRY, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_JSRQ, GetType(System.DateTime))


                    .Add(FIELD_GW_V_YIJIAOWENJIAN_SFYJ, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_SFJS, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJLX, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_BLLX, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJZL, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_ZSDW, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJBT, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJZH, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_JGDZ, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJNF, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJXH, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_MMDJ, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_JJCD, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_WJND, GetType(System.Int32))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_ZBDW, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_NGR, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_NGRQ, GetType(System.DateTime))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_BLZT, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_LSH, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_ZTC, GetType(System.String))
                    .Add(FIELD_GW_V_YIJIAOWENJIAN_KSSW, GetType(System.Int32))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_YijiaoWenjian = table

        End Function


    End Class 'FlowData

End Namespace 'Xydc.Platform.Common.Data
