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
    ' 类名    ：CustomerData
    '
    ' 功能描述：
    '     定义与人员信息相关表的数据访问格式
    '----------------------------------------------------------------
    <System.ComponentModel.DesignerCategory("BASE"), SerializableAttribute()> Public Class CustomerData
        Inherits System.Data.DataSet

        '公共参数
        Public Const STATUS_LOGIN As String = "登录"
        Public Const STATUS_LOGOUT As String = "退出"

        '公共_B_人员表信息定义
        '表名称
        Public Const TABLE_GG_B_RENYUAN As String = "公共_B_人员"
        '字段序列
        Public Const FIELD_GG_B_RENYUAN_RYDM As String = "人员代码"
        Public Const FIELD_GG_B_RENYUAN_RYMC As String = "人员名称"
        Public Const FIELD_GG_B_RENYUAN_RYXH As String = "人员序号"
        Public Const FIELD_GG_B_RENYUAN_ZZDM As String = "组织代码"
        Public Const FIELD_GG_B_RENYUAN_JBDM As String = "级别代码"
        Public Const FIELD_GG_B_RENYUAN_MSDM As String = "秘书代码"
        Public Const FIELD_GG_B_RENYUAN_LXDH As String = "联系电话"
        Public Const FIELD_GG_B_RENYUAN_SJHM As String = "手机号码"
        Public Const FIELD_GG_B_RENYUAN_FTPDZ As String = "FTP地址"
        Public Const FIELD_GG_B_RENYUAN_YXDZ As String = "邮箱地址"
        Public Const FIELD_GG_B_RENYUAN_ZDQS As String = "自动签收"
        Public Const FIELD_GG_B_RENYUAN_JJXSMC As String = "交接显示名称"
        Public Const FIELD_GG_B_RENYUAN_KCKXM As String = "可查看姓名"
        Public Const FIELD_GG_B_RENYUAN_KZSRY As String = "可直送人员"
        Public Const FIELD_GG_B_RENYUAN_QTYZS As String = "其他由转送"
        Public Const FIELD_GG_B_RENYUAN_SFJM As String = "是否加密"

        Public Const FIELD_GG_B_RENYUAN_RYZM As String = "人员真名"


        '约束错误信息

        '公共_B_组织机构表信息定义
        '表名称
        Public Const TABLE_GG_B_ZUZHIJIGOU As String = "公共_B_组织机构"
        '字段序列
        Public Const FIELD_GG_B_ZUZHIJIGOU_ZZDM As String = "组织代码"
        Public Const FIELD_GG_B_ZUZHIJIGOU_ZZMC As String = "组织名称"
        Public Const FIELD_GG_B_ZUZHIJIGOU_ZZBM As String = "组织别名"
        Public Const FIELD_GG_B_ZUZHIJIGOU_JBDM As String = "级别代码"
        Public Const FIELD_GG_B_ZUZHIJIGOU_MSDM As String = "秘书代码"
        Public Const FIELD_GG_B_ZUZHIJIGOU_LXDH As String = "联系电话"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SJHM As String = "手机号码"
        Public Const FIELD_GG_B_ZUZHIJIGOU_FTPDZ As String = "FTP地址"
        Public Const FIELD_GG_B_ZUZHIJIGOU_YXDZ As String = "邮箱地址"
        Public Const FIELD_GG_B_ZUZHIJIGOU_LXDZ As String = "联系地址"
        Public Const FIELD_GG_B_ZUZHIJIGOU_YZBM As String = "邮政编码"
        Public Const FIELD_GG_B_ZUZHIJIGOU_LXR As String = "联系人"
        '约束错误信息

        '公共_B_上岗表信息定义
        '表名称
        Public Const TABLE_GG_B_SHANGGANG As String = "公共_B_上岗"
        '字段序列
        Public Const FIELD_GG_B_SHANGGANG_RYDM As String = "人员代码"
        Public Const FIELD_GG_B_SHANGGANG_GWDM As String = "岗位代码"
        '约束错误信息

        '公共_B_人员表的完全连接
        '表名称
        Public Const TABLE_GG_B_RENYUAN_FULLJOIN As String = "公共_B_人员完全连接表"
        '字段序列
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_GWLB As String = "岗位列表"
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_MSMC As String = "秘书名称"
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_SFSQ As String = "是否申请"
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_QTYZSMC As String = "其他由转送名称"


        '显示字段
        Public Const FIELD_GG_B_RENYUAN_FULLJOIN_BH As String = "编号"


        '人员/单位/范围选择表信息
        '表名称
        Public Const TABLE_GG_B_RENYUAN_SELECT As String = "公共_B_人员单位范围选择表"
        '字段序列
        Public Const FIELD_GG_B_RENYUAN_SELECT_MC As String = "名称"
        Public Const FIELD_GG_B_RENYUAN_SELECT_LX As String = "类型"
        Public Const FIELD_GG_B_RENYUAN_SELECT_XH As String = "序号"
        Public Const FIELD_GG_B_RENYUAN_SELECT_BM As String = "部门"
        Public Const FIELD_GG_B_RENYUAN_SELECT_ZW As String = "职务"
        Public Const FIELD_GG_B_RENYUAN_SELECT_JB As String = "级别"
        Public Const FIELD_GG_B_RENYUAN_SELECT_MS As String = "秘书"
        Public Const FIELD_GG_B_RENYUAN_SELECT_LXDH As String = "联系电话"
        Public Const FIELD_GG_B_RENYUAN_SELECT_SJHM As String = "手机号码"
        Public Const FIELD_GG_B_RENYUAN_SELECT_FTPDZ As String = "FTP地址"
        Public Const FIELD_GG_B_RENYUAN_SELECT_YXDZ As String = "邮箱地址"

        '单位/范围选择表信息
        '表名称
        Public Const TABLE_GG_B_ZUZHIJIGOU_SELECT As String = "公共_B_单位范围选择表"
        '字段序列
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC As String = "单位名称"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX As String = "选择类型"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC As String = "单位全称"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB As String = "单位级别"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS As String = "单位秘书"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH As String = "联系电话"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM As String = "手机号码"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ As String = "FTP地址"
        Public Const FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ As String = "邮箱地址"

        '公共_B_组织机构表的完全连接
        '表名称
        Public Const TABLE_GG_B_ZUZHIJIGOU_FULLJOIN As String = "公共_B_组织机构完全连接表"
        '字段序列
        Public Const FIELD_GG_B_ZUZHIJIGOU_FULLJOIN_LXRMC As String = "联系人名称"

        '“管理_B_系统进出日志”信息
        '表名称
        Public Const TABLE_GL_B_XITONGJINCHURIZHI As String = "管理_B_系统进出日志"
        '字段序列
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_XH As String = "序号"
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_CZR As String = "操作人"
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_CZSJ As String = "操作时间"
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_CZLX As String = "操作类型"
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_JQDZ As String = "机器地址"

        Public Const FIELD_GL_B_XITONGJINCHURIZHI_JQMC As String = "机器名称"


        '显示字段序列
        Public Const FIELD_GL_B_XITONGJINCHURIZHI_CZRMC As String = "操作人名称"

        '“管理_B_在线用户”信息
        '表名称
        Public Const TABLE_GL_B_ZAIXIANYONGHU As String = "管理_B_在线用户"
        '字段序列
        Public Const FIELD_GL_B_ZAIXIANYONGHU_CZR As String = "操作人"
        Public Const FIELD_GL_B_ZAIXIANYONGHU_SXSJ As String = "上线时间"
        '显示字段序列
        Public Const FIELD_GL_B_ZAIXIANYONGHU_CZRMC As String = "操作人名称"
        Public Const FIELD_GL_B_ZAIXIANYONGHU_SXSC As String = "上线时长"

        '“管理_B_用户操作日志”信息
        '表名称
        Public Const TABLE_GL_B_YONGHUCAOZUORIZHI As String = "管理_B_用户操作日志"
        '字段序列
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_XH As String = "序号"
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_CZR As String = "操作人"
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_CZSJ As String = "操作时间"
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_JQDZ As String = "机器地址"
        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_CZSM As String = "操作说明"

        Public Const FIELD_GL_B_YONGHUCAOZUORIZHI_JQMC As String = "机器名称"


        '显示字段序列








        '定义初始化表类型enum
        Public Enum enumTableType
            GG_B_RENYUAN = 1
            GG_B_RENYUAN_SELECT = 2
            GG_B_RENYUAN_FULLJOIN = 3
            GG_B_ZUZHIJIGOU = 4
            GG_B_ZUZHIJIGOU_SELECT = 5
            GG_B_ZUZHIJIGOU_FULLJOIN = 6
            GG_B_SHANGGANG = 7
            GL_B_XITONGJINCHURIZHI = 8
            GL_B_ZAIXIANYONGHU = 9
            GL_B_YONGHUCAOZUORIZHI = 10
        End Enum

        '组织代码分级长度说明
        Public Shared intZZDM_FJCDSM() As Integer = {2, 4, 6, 8, 10, 12}









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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Data.CustomerData)
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
                Case enumTableType.GG_B_RENYUAN
                    table = createDataTables_Renyuan(strErrMsg)
                Case enumTableType.GG_B_RENYUAN_SELECT
                    table = createDataTables_Renyuan_Select(strErrMsg)
                Case enumTableType.GG_B_RENYUAN_FULLJOIN
                    table = createDataTables_Renyuan_FullJoin(strErrMsg)

                Case enumTableType.GG_B_ZUZHIJIGOU
                    table = createDataTables_Zuzhijigou(strErrMsg)
                Case enumTableType.GG_B_ZUZHIJIGOU_SELECT
                    table = createDataTables_Zuzhijigou_Select(strErrMsg)
                Case enumTableType.GG_B_ZUZHIJIGOU_FULLJOIN
                    table = createDataTables_Zuzhijigou_FullJoin(strErrMsg)

                Case enumTableType.GG_B_SHANGGANG
                    table = createDataTables_Shanggang(strErrMsg)

                Case enumTableType.GL_B_XITONGJINCHURIZHI
                    table = createDataTables_Xitongjinchurizhi(strErrMsg)
                Case enumTableType.GL_B_ZAIXIANYONGHU
                    table = createDataTables_Zaixianyonghu(strErrMsg)
                Case enumTableType.GL_B_YONGHUCAOZUORIZHI
                    table = createDataTables_YonghuCaozuoRizhi(strErrMsg)

                Case Else
                    strErrMsg = "无效的表类型！"
                    table = Nothing
            End Select

            createDataTables = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_RENYUAN
        '----------------------------------------------------------------
        Private Function createDataTables_Renyuan(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_RENYUAN)
                With table.Columns
                    .Add(FIELD_GG_B_RENYUAN_RYDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_RYMC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_RYXH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_ZZDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_MSDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_YXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_ZDQS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_JJXSMC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_KCKXM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_KZSRY, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_QTYZS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SFJM, GetType(System.Int32))

                    .Add(FIELD_GG_B_RENYUAN_RYZM, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Renyuan = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_ZUZHIJIGOU
        '----------------------------------------------------------------
        Private Function createDataTables_Zuzhijigou(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_ZUZHIJIGOU)
                With table.Columns
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZMC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZBM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_MSDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_YXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_YZBM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXR, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zuzhijigou = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_SHANGGANG
        '----------------------------------------------------------------
        Private Function createDataTables_Shanggang(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_SHANGGANG)
                With table.Columns
                    .Add(FIELD_GG_B_SHANGGANG_RYDM, GetType(System.String))
                    .Add(FIELD_GG_B_SHANGGANG_GWDM, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Shanggang = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_RENYUAN_FULLJOIN
        '----------------------------------------------------------------
        Private Function createDataTables_Renyuan_FullJoin(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_RENYUAN_FULLJOIN)
                With table.Columns


                    '公共_B_人员全部字段

                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_BH, GetType(System.String))

                    .Add(FIELD_GG_B_RENYUAN_RYDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_RYMC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_RYXH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_ZZDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_MSDM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_YXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_ZDQS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_JJXSMC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_KCKXM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_KZSRY, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_QTYZS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SFJM, GetType(System.Int32))

                    '公共_B_组织机构表的组织名称、组织别名
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZMC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZBM, GetType(System.String))

                    '公共_B_上岗表对应的公共_B_工作岗位中的岗位名称集合（分号分隔）
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_GWLB, GetType(System.String))

                    '公共_B_行政级别中的级别名称、行政级别
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_XZJB, GetType(System.Int32))

                    '公共_B_人员中检索出的秘书名称
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_MSMC, GetType(System.String))

                    '公共_B_人员中检索出的其他由转送人名称
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_QTYZSMC, GetType(System.String))

                    '是否申请ID?
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_SFSQ, GetType(System.String))

                    .Add(FIELD_GG_B_RENYUAN_RYZM, GetType(System.String))



                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Renyuan_FullJoin = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_RENYUAN_SELECT
        '----------------------------------------------------------------
        Private Function createDataTables_Renyuan_Select(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_RENYUAN_SELECT)
                With table.Columns
                    .Add(FIELD_GG_B_RENYUAN_SELECT_MC, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_LX, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_XH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_BM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_ZW, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_JB, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_MS, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_RENYUAN_SELECT_YXDZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Renyuan_Select = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_ZUZHIJIGOU_FULLJOIN
        '----------------------------------------------------------------
        Private Function createDataTables_Zuzhijigou_FullJoin(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_ZUZHIJIGOU_FULLJOIN)
                With table.Columns
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZMC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_ZZBM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_JBDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_MSDM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_YXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_YZBM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_LXR, GetType(System.String))

                    '公共_B_行政级别中的级别名称、行政级别
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_JBMC, GetType(System.String))
                    .Add(Xydc.Platform.Common.Data.XingzhengjibieData.FIELD_GG_B_XINGZHENGJIBIE_XZJB, GetType(System.Int32))

                    '公共_B_人员中检索出的秘书名称
                    .Add(FIELD_GG_B_RENYUAN_FULLJOIN_MSMC, GetType(System.String))

                    '公共_B_人员中检索出的联系人名称
                    .Add(FIELD_GG_B_ZUZHIJIGOU_FULLJOIN_LXRMC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zuzhijigou_FullJoin = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GG_B_ZUZHIJIGOU_SELECT
        '----------------------------------------------------------------
        Private Function createDataTables_Zuzhijigou_Select(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GG_B_ZUZHIJIGOU_SELECT)
                With table.Columns
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_XZLX, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_DWQC, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_DWJB, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_DWMS, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_LXDH, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_SJHM, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_FTPDZ, GetType(System.String))
                    .Add(FIELD_GG_B_ZUZHIJIGOU_SELECT_YXDZ, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zuzhijigou_Select = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GL_B_XITONGJINCHURIZHI
        '----------------------------------------------------------------
        Private Function createDataTables_Xitongjinchurizhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_XITONGJINCHURIZHI)
                With table.Columns
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_XH, GetType(System.Int32))
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_CZR, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_CZSJ, GetType(System.DateTime))
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_CZLX, GetType(System.String))
                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_JQDZ, GetType(System.String))

                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_CZRMC, GetType(System.String))

                    .Add(FIELD_GL_B_XITONGJINCHURIZHI_JQMC, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Xitongjinchurizhi = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GL_B_ZAIXIANYONGHU
        '----------------------------------------------------------------
        Private Function createDataTables_Zaixianyonghu(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_ZAIXIANYONGHU)
                With table.Columns
                    .Add(FIELD_GL_B_ZAIXIANYONGHU_CZR, GetType(System.String))
                    .Add(FIELD_GL_B_ZAIXIANYONGHU_SXSJ, GetType(System.DateTime))

                    .Add(FIELD_GL_B_ZAIXIANYONGHU_CZRMC, GetType(System.String))
                    .Add(FIELD_GL_B_ZAIXIANYONGHU_SXSC, GetType(System.String))
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_Zaixianyonghu = table

        End Function

        '----------------------------------------------------------------
        '创建TABLE_GL_B_YONGHUCAOZUORIZHI
        '----------------------------------------------------------------
        Private Function createDataTables_YonghuCaozuoRizhi(ByRef strErrMsg As String) As System.Data.DataTable

            Dim table As System.Data.DataTable

            Try
                table = New DataTable(TABLE_GL_B_YONGHUCAOZUORIZHI)
                With table.Columns
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_XH, GetType(System.Int32))
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_CZR, GetType(System.String))
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_CZSJ, GetType(System.DateTime))
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_JQDZ, GetType(System.String))
                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_CZSM, GetType(System.String))

                    .Add(FIELD_GL_B_YONGHUCAOZUORIZHI_JQMC, GetType(System.String))

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                table = Nothing
            End Try

            createDataTables_YonghuCaozuoRizhi = table

        End Function

    End Class 'CustomerData

End Namespace 'Xydc.Platform.Common.Data
