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
Imports System.Text.RegularExpressions
Imports Microsoft.VisualBasic

Imports Xydc.Platform.SystemFramework
Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.DataAccess

Namespace Xydc.Platform.BusinessRules

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessRules
    ' 类名    ：rulesCustomer
    '
    ' 功能描述： 
    '   　提供对人员信息处理的业务规则
    '----------------------------------------------------------------
    Public Class rulesCustomer

        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessRules.rulesCustomer)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' 输出数据到Excel
        '     strErrMsg            ：如果错误，则返回错误信息
        '     objDataSet           ：要导出的数据集
        '     strExcelFile         ：导出到WEB服务器中的Excel文件路径
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Overridable Function doExportToExcel( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strExcelFile As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doExportToExcel = .doExportToExcel(strErrMsg, objDataSet, strExcelFile)
                End With
            Catch ex As Exception
                doExportToExcel = False
                strErrMsg = ex.Message
            End Try

        End Function









        '----------------------------------------------------------------
        ' 验证用户与密码是否匹配？首先加密后的密码验证，如果验证成功则返回；
        ' 否则对明码进行验证：成功则对密码进行加密并自动更改为加密密码，
        ' 不成功则返回错误。如果用户=SA，则不加密
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：要验证的用户标识
        '     strPassword   ：要验证的用户的密码(用户输入的密码-明码)
        '     strNewPassword：返回验证后的新密码(加密后的密码)
        ' 返回
        '     True          ：用户与密码一致
        '     False         ：用户与密码不匹配
        '----------------------------------------------------------------
        Public Function doVerifyUserPassword( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef strNewPassword As String) As Boolean

            Dim objdacCustomer As Xydc.Platform.DataAccess.dacCustomer

            '初始化
            doVerifyUserPassword = False
            strNewPassword = strPassword
            If strUserId Is Nothing Then strUserId = ""

            '检查
            If strUserId.Trim = "" Then
                strErrMsg = "未指定用户！"
                GoTo errProc
            End If

            '验证
            Dim strEncryptPassword As String
            Try
                objdacCustomer = New Xydc.Platform.DataAccess.dacCustomer
                With objdacCustomer
                    '密码已经加密？
                    If strUserId.ToUpper() <> "SA" Then
                        '获取加密密码
                        strEncryptPassword = .doEncryptPassowrd(strPassword)
                        '验证加密密码
                        .doVerifyUserPassword(strErrMsg, strUserId, strEncryptPassword)
                        If strErrMsg = "" Then
                            strNewPassword = strEncryptPassword
                            GoTo normExit
                        End If
                    End If

                    '密码未加密
                    .doVerifyUserPassword(strErrMsg, strUserId, strPassword)
                    If strErrMsg <> "" Then
                        GoTo errProc
                    End If

                    '对密码进行加密
                    If strUserId.ToUpper() <> "SA" Then
                        '更改明码
                        .doModifyUserPassword(strErrMsg, strUserId, strPassword, strUserId, strEncryptPassword)
                        If strErrMsg <> "" Then
                            GoTo errProc
                        End If
                        strNewPassword = strEncryptPassword
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

normExit:
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)

            '返回
            doVerifyUserPassword = True
            Exit Function

errProc:
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 验证数据库连接串
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strConnect    ：要验证的连接串
        ' 返回
        '     True          ：有效
        '     False         ：无效
        '----------------------------------------------------------------
        Public Function doVerifyConnectionString( _
            ByRef strErrMsg As String, _
            ByVal strConnect As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doVerifyConnectionString = .doVerifyConnectionString(strErrMsg, strConnect)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                doVerifyConnectionString = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' 更改用户密码：如果strCzyId=strUserId，则自己更改自己的密码，
        ' 否则为SA强制更改strUserId的密码。成功返回加密后的新密码，
        ' 不成功则返回错误。如果用户=SA，则不加密
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strCzyId       ：当前操作员
        '     strCzyPassword ：当前操作员的密码
        '     strUserId      ：要更改密码的用户标识
        '     strNewPassword1：新密码1
        '     strNewPassword2：新密码2
        '     strNewPassword ：返回加密后的新密码
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function doModifyPassword( _
            ByRef strErrMsg As String, _
            ByVal strCzyId As String, _
            ByVal strCzyPassword As String, _
            ByVal strUserId As String, _
            ByVal strNewPassword1 As String, _
            ByVal strNewPassword2 As String, _
            ByRef strNewPassword As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objdacCustomer As Xydc.Platform.DataAccess.dacCustomer

            '初始化
            doModifyPassword = False
            If strCzyId.Length > 0 Then strCzyId = strCzyId.Trim()
            If strCzyPassword.Length > 0 Then strCzyPassword = strCzyPassword.Trim()
            If strUserId Is Nothing Then strUserId = ""
            If strNewPassword1.Length > 0 Then strNewPassword1 = strNewPassword1.Trim()
            If strNewPassword2.Length > 0 Then strNewPassword2 = strNewPassword2.Trim()

            '检查
            If strCzyId.Length < 1 Then
                strErrMsg = "未指定当前操作人员！"
                GoTo errProc
            End If
            If strUserId.Trim = "" Then
                strErrMsg = "未指定要更改密码的用户！"
                GoTo errProc
            End If
            If strNewPassword1.Length > 0 And strNewPassword2.Length > 0 Then
                If strNewPassword1 <> strNewPassword2 Then
                    strErrMsg = "两次输入的密码不一致！"
                    GoTo errProc
                End If
            End If

            '更改密码
            Dim strEncryptPassword As String
            Try
                objdacCustomer = New Xydc.Platform.DataAccess.dacCustomer
                With objdacCustomer
                    '获取加密密码
                    If strUserId.ToUpper() = "SA" Then
                        strEncryptPassword = strNewPassword1
                    Else
                        strEncryptPassword = .doEncryptPassowrd(strNewPassword1)
                    End If
                    '设置新密码
                    .doModifyUserPassword(strErrMsg, strCzyId, strCzyPassword, strUserId, strEncryptPassword)
                    If strErrMsg <> "" Then
                        GoTo errProc
                    End If
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)

            '返回
            strNewPassword = strEncryptPassword
            doModifyPassword = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.DataAccess.dacCustomer.SafeRelease(objdacCustomer)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' 获取完整用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strWhere       ：搜索条件
        '     blnUnused      ：重载用
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByVal blnUnused As Boolean, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strWhere, blnUnused, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据用户Id获取用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strOptions     ：获取数据选项ABCD
        '                      A=1 获取人员单表数据
        '                      B=1 获取人员的组织机构单表数据
        '                      C=1 获取人员的上岗单表数据
        '                      D=1 获取人员的完全连接的表数据
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strOptions, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据strRYDM获取用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strRYDM        ：人员代码
        '     strOptions     ：获取数据选项ABCD
        '                      A=1 获取人员单表数据
        '                      B=1 获取人员的组织机构单表数据
        '                      C=1 获取人员的上岗单表数据
        '                      D=1 获取人员的完全连接的表数据
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strRYDM, strOptions, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据strRYMC获取用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strRYDM        ：人员代码
        '     strRYMC        ：人员名称
        '     strOptions     ：获取数据选项ABCD
        '                      A=1 获取人员单表数据
        '                      B=1 获取人员的组织机构单表数据
        '                      C=1 获取人员的上岗单表数据
        '                      D=1 获取人员的完全连接的表数据
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strRYMC As String, _
            ByVal strOptions As String, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strRYDM, strRYMC, strOptions, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据strRYDM获取用户信息数据集
        '     strErrMsg      ：如果错误，则返回错误信息
        '     strUserId      ：用户标识
        '     strPassword    ：用户密码
        '     strRYDM        ：人员代码
        '     strZZDM        ：要获取的组织代码
        '     strOptions     ：获取数据选项ABCD
        '                      A=1 获取人员单表数据
        '                      B=1 获取人员的组织机构单表数据
        '                      C=1 获取人员的上岗单表数据
        '                      D=1 获取人员的完全连接的表数据
        '     blnUser        ：重载
        '     objCustomerData：用户信息数据集
        ' 返回
        '     True           ：成功
        '     False          ：失败

        '----------------------------------------------------------------
        Public Function getRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByVal strZZDM As String, _
            ByVal strOptions As String, _
            ByVal blnUser As Boolean, _
            ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getRenyuanData = .getRenyuanData(strErrMsg, strUserId, strPassword, strRYDM, strZZDM, strOptions, blnUser, objCustomerData)
                End With
            Catch ex As Exception
                getRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取组织机构信息数据集(以组织代码升序排序,不含连接数据)
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据组织代码获取组织机构全连接信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：组织代码
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据组织代码获取组织机构单表信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：组织代码
        '     blnUnused            ：重载用
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal blnUnused As Boolean, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, blnUnused, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据组织名称获取组织机构全连接信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：组织代码(接口重载用)
        '     strZZMC              ：组织名称
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal strZZMC As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, strZZMC, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据组织名称获取组织机构单表信息数据集
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     blnUnused            ：重载用
        '     strZZMC              ：组织名称
        '     objBumenData         ：组织机构信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal blnUnused As Boolean, _
            ByVal strZZMC As String, _
            ByRef objBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getBumenData = .getBumenData(strErrMsg, strUserId, strPassword, blnUnused, strZZMC, objBumenData)
                End With
            Catch ex As Exception
                getBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取指定组织机构下的人员信息数据集(以组织代码、人员序号升序排序)
        ' 含人员的全部连接数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：指定组织机构代码
        '     blnBaohanXiaji       ：是否包含下级部门
        '     strWhere             ：搜索字符串(默认表前缀a.)
        '     objRenyuanData       ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanInBumenData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByVal blnBaohanXiaji As Boolean, _
            ByVal strWhere As String, _
            ByRef objRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getRenyuanInBumenData = .getRenyuanInBumenData(strErrMsg, strUserId, strPassword, strZZDM, blnBaohanXiaji, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                getRenyuanInBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据指定范围名称获取范围下的组织信息或人员信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFWMC              ：指定范围名称
        '     blnAllowBM           ：允许部门信息直接选择
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objSelectRenyuanData ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getRenyuanOrBumenInFanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByVal blnAllowBM As Boolean, _
            ByVal strWhere As String, _
            ByRef objSelectRenyuanData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getRenyuanOrBumenInFanweiData = .getRenyuanOrBumenInFanweiData(strErrMsg, strUserId, strPassword, strFWMC, blnAllowBM, strWhere, objSelectRenyuanData)
                End With
            Catch ex As Exception
                getRenyuanOrBumenInFanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据指定范围名称获取范围下的组织信息
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strFWMC              ：指定范围名称
        '     strWhere             ：搜索条件(默认表前缀a.)
        '     objSelectBumenData   ：指定组织机构下的人员信息数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getBumenInFanweiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strFWMC As String, _
            ByVal strWhere As String, _
            ByRef objSelectBumenData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getBumenInFanweiData = .getBumenInFanweiData(strErrMsg, strUserId, strPassword, strFWMC, strWhere, objSelectBumenData)
                End With
            Catch ex As Exception
                getBumenInFanweiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strBLR、strWTR能直接发送的人员代码列表的SQL语句
        '     strBLR               ：当前办理人的名称
        '     strWTRArray          ：strBLR受strWTR委托进行处理
        ' 返回
        '                          ：SQL语句
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTRArray As String()) As String

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getSendRestrictWhere = .getSendRestrictWhere(strBLR, strWTRArray)
                End With
            Catch ex As Exception
                getSendRestrictWhere = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strBLR、strWTR能直接发送的人员代码列表的SQL语句
        '     strBLR               ：当前办理人的名称
        '     strWTR               ：strBLR受strWTR委托进行处理
        ' 返回
        '                          ：SQL语句
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTR As String) As String

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getSendRestrictWhere = .getSendRestrictWhere(strBLR, strWTR)
                End With
            Catch ex As Exception
                getSendRestrictWhere = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取strBLR、strWTR能直接发送的人员代码列表的SQL语句
        '     strBLR               ：当前办理人的名称
        '     strWTR               ：strBLR受strWTR委托进行处理
        '     blnByRYDM            ：指定的是人员代码
        ' 返回
        '                          ：SQL语句
        '----------------------------------------------------------------
        Public Function getSendRestrictWhere( _
            ByVal strBLR As String, _
            ByVal strWTR As String, _
            ByVal blnByRYDM As Boolean) As String

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getSendRestrictWhere = .getSendRestrictWhere(strBLR, strWTR, blnByRYDM)
                End With
            Catch ex As Exception
                getSendRestrictWhere = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据人员名称获取人员代码
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strRYMC       ：人员名称
        '     strRYDM       ：人员代码(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getRydmByRymc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYMC As String, _
            ByRef strRYDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData

            getRydmByRymc = False
            strRYDM = ""

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .getRenyuanData(strErrMsg, strUserId, strPassword, "", strRYMC, "1000", objCustomerData) = False Then
                        Exit Try
                    End If
                End With

                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN)
                    If .Rows.Count > 0 Then
                        strRYDM = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYDM), "")
                    End If
                End With

                getRydmByRymc = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)

        End Function

        '----------------------------------------------------------------
        ' 根据人员代码获取人员名称
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strRYDM       ：人员代码
        '     strRYMC       ：人员名称(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getRymcByRydm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef strRYMC As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData

            getRymcByRydm = False
            strRYMC = ""

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .getRenyuanData(strErrMsg, strUserId, strPassword, strRYDM, "1000", objCustomerData) = False Then
                        Exit Try
                    End If
                End With

                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN)
                    If .Rows.Count > 0 Then
                        strRYMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC), "")
                    End If
                End With

                getRymcByRydm = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)

        End Function

        '----------------------------------------------------------------
        ' 根据单位名称获取单位代码
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strZZMC       ：单位名称
        '     strZZDM       ：单位代码(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getZzdmByZzmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZMC As String, _
            ByRef strZZDM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData

            getZzdmByZzmc = False
            strZZDM = ""

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .getBumenData(strErrMsg, strUserId, strPassword, True, strZZMC, objCustomerData) = False Then
                        Exit Try
                    End If
                End With

                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU)
                    If .Rows.Count > 0 Then
                        strZZDM = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZDM), "")
                    End If
                End With

                getZzdmByZzmc = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)

        End Function

        '----------------------------------------------------------------
        ' 根据单位名称获取单位别名(全称)
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strZZMC       ：单位名称
        '     strZZBM       ：单位别名(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getZzbmByZzmc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZMC As String, _
            ByRef strZZBM As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData

            getZzbmByZzmc = False
            strZZBM = ""

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .getBumenData(strErrMsg, strUserId, strPassword, True, strZZMC, objCustomerData) = False Then
                        Exit Try
                    End If
                End With

                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU)
                    If .Rows.Count > 0 Then
                        strZZBM = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZBM), "")
                    End If
                End With

                getZzbmByZzmc = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)

        End Function

        '----------------------------------------------------------------
        ' 根据单位代码获取单位名称
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strZZDM       ：单位代码
        '     strZZMC       ：单位名称(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getZzmcByZzdm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef strZZMC As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData

            getZzmcByZzdm = False
            strZZMC = ""

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, True, objCustomerData) = False Then
                        Exit Try
                    End If
                End With

                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU)
                    If .Rows.Count > 0 Then
                        strZZMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                    End If
                End With

                getZzmcByZzdm = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)

        End Function

        '----------------------------------------------------------------
        ' 根据单位代码获取组织名称，组织别名
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strZZDM       ：单位代码
        '     strBMXX()     ：strBMXX(0)=组织名称,strBMXX(1)=组织别名(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getZzmcByZzbm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef strBMXX() As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData

            getZzmcByZzbm = False


            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .getFWBumenData(strErrMsg, strUserId, strPassword, strZZDM, objCustomerData) = False Then
                        Exit Try
                    End If
                End With

                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU)
                    If .Rows.Count > 0 Then
                        strBMXX(0) = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                        strBMXX(1) = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZBM), "")
                    End If
                End With

                getZzmcByZzbm = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)

        End Function

        '----------------------------------------------------------------
        ' 根据人员代码获取单位名称
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strRYDM       ：人员代码
        '     strZZMC       ：单位名称(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getZzmcByRydm( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYDM As String, _
            ByRef strZZMC As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData

            getZzmcByRydm = False
            strZZMC = ""

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .getRenyuanData(strErrMsg, strUserId, strPassword, strRYDM, "1000", objCustomerData) = False Then
                        Exit Try
                    End If
                    Dim strZZDM As String = ""
                    With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN)
                        If .Rows.Count < 1 Then Exit Try
                        strZZDM = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")
                    End With
                    objCustomerData.Dispose()
                    objCustomerData = Nothing
                    If .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, True, objCustomerData) = False Then
                        Exit Try
                    End If
                End With

                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU)
                    If .Rows.Count > 0 Then
                        strZZMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                    End If
                End With

                getZzmcByRydm = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)

        End Function

        '----------------------------------------------------------------
        ' 根据人员名称获取单位名称
        '     strErrMsg     ：如果错误，则返回错误信息
        '     strUserId     ：用户标识
        '     strPassword   ：用户密码
        '     strRYMC       ：人员名称
        '     strZZMC       ：单位名称(返回)
        ' 返回
        '     True          ：成功
        '     False         ：失败
        '----------------------------------------------------------------
        Public Function getZzmcByRymc( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strRYMC As String, _
            ByRef strZZMC As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objCustomerData As Xydc.Platform.Common.Data.CustomerData

            getZzmcByRymc = False
            strZZMC = ""

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .getRenyuanData(strErrMsg, strUserId, strPassword, "", strRYMC, "1000", objCustomerData) = False Then
                        Exit Try
                    End If
                    Dim strZZDM As String = ""
                    With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN)
                        If .Rows.Count < 1 Then Exit Try
                        strZZDM = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), "")
                    End With
                    objCustomerData.Dispose()
                    objCustomerData = Nothing
                    If .getBumenData(strErrMsg, strUserId, strPassword, strZZDM, True, objCustomerData) = False Then
                        Exit Try
                    End If
                End With

                With objCustomerData.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_ZUZHIJIGOU)
                    If .Rows.Count > 0 Then
                        strZZMC = objPulicParameters.getObjectValue(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), "")
                    End If
                End With

                getZzmcByRymc = True

            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Data.CustomerData.SafeRelease(objCustomerData)

        End Function

        '----------------------------------------------------------------
        ' 根据指定上级代码获取下级代码值
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strSJDM              ：上级代码
        '     intFJCDSM            ：代码分级长度
        '     strNewZZDM           ：新代码（返回）
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewZZDM( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strSJDM As String, _
            ByVal intFJCDSM() As Integer, _
            ByRef strNewZZDM As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getNewZZDM = .getNewZZDM(strErrMsg, strUserId, strPassword, strSJDM, intFJCDSM, strNewZZDM)
                End With
            Catch ex As Exception
                getNewZZDM = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_组织机构”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveZuzhijigouData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType) As Boolean

            doSaveZuzhijigouData = False
            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .doVerifyZuzhijigouData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                        Exit Try
                    End If
                    doSaveZuzhijigouData = .doSaveZuzhijigouData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“公共_B_组织机构”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteZuzhijigouData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doDeleteZuzhijigouData = .doDeleteZuzhijigouData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteZuzhijigouData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取新的人员序号
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strZZDM              ：给定组织代码
        '     strNewRYXH           ：新人员序号(返回)
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function getNewRYXH( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strZZDM As String, _
            ByRef strNewRYXH As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getNewRYXH = .getNewRYXH(strErrMsg, strUserId, strPassword, strZZDM, strNewRYXH)
                End With
            Catch ex As Exception
                getNewRYXH = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_人员”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objenumEditType      ：编辑类型
        '     objNewDataSG         ：上岗数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doSaveRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewDataSG As Xydc.Platform.Common.Data.CustomerData) As Boolean

            doSaveRenyuanData = False
            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .doVerifyRenyuanData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                        Exit Try
                    End If
                    doSaveRenyuanData = .doSaveRenyuanData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType, objNewDataSG)
                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 保存“公共_B_人员_兼任”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        '     objNewData           ：新数据
        '     objUpdateData        ：更新“公共_B_人员”数据 
        '     objenumEditType      ：编辑类型
        '     objNewDataSG         ：上岗数据集
        ' 返回
        '     True                 ：成功
        '     False                ：失败

        '----------------------------------------------------------------
        Public Function doSaveRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow, _
            ByVal objNewData As System.Collections.Specialized.NameValueCollection, _
            ByVal objUpdateData As System.Collections.Specialized.NameValueCollection, _
            ByVal objenumEditType As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType, _
            ByVal objNewDataSG As Xydc.Platform.Common.Data.CustomerData) As Boolean

            doSaveRenyuanData = False
            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    If .doVerifyRenyuanData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType) = False Then
                        Exit Try
                    End If
                    doSaveRenyuanData = .doSaveRenyuanData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objUpdateData, objenumEditType, objNewDataSG)

                End With
            Catch ex As Exception
                strErrMsg = ex.Message
            End Try

        End Function


        '----------------------------------------------------------------
        ' 删除“公共_B_人员”的数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objOldData           ：旧数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteRenyuanData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objOldData As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doDeleteRenyuanData = .doDeleteRenyuanData(strErrMsg, strUserId, strPassword, objOldData)
                End With
            Catch ex As Exception
                doDeleteRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 将指定人员objRenyuanData位置移动到objRenyuanDataTo
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     objRenyuanData       ：准备移动的人员数据
        '     objRenyuanDataTo     ：移动到的人员数据
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doRenyuanMoveTo( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal objRenyuanData As System.Data.DataRow, _
            ByVal objRenyuanDataTo As System.Data.DataRow) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doRenyuanMoveTo = .doRenyuanMoveTo(strErrMsg, strUserId, strPassword, objRenyuanData, objRenyuanDataTo)
                End With
            Catch ex As Exception
                doRenyuanMoveTo = False
                strErrMsg = ex.Message
            End Try

        End Function




        '----------------------------------------------------------------
        ' 获取系统进出日志数据
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strWhere                 ：搜索条件
        '     objXitongJinchuRizhiData ：系统进出日志信息数据集
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getXitongJinchuRizhiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objXitongJinchuRizhiData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getXitongJinchuRizhiData = .getXitongJinchuRizhiData(strErrMsg, strUserId, strPassword, strWhere, objXitongJinchuRizhiData)
                End With
            Catch ex As Exception
                getXitongJinchuRizhiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取系统在线用户数据
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strWhere                 ：搜索条件
        '     objZaixianYonghuData     ：在线用户信息数据集
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getZaixianYonghuData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objZaixianYonghuData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getZaixianYonghuData = .getZaixianYonghuData(strErrMsg, strUserId, strPassword, strWhere, objZaixianYonghuData)
                End With
            Catch ex As Exception
                getZaixianYonghuData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 写“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCZLX              ：操作类型
        '     strAddress           ：机器地址
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doWriteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCZLX As String, _
            ByVal strAddress As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doWriteXitongJinchuRizhi = .doWriteXitongJinchuRizhi(strErrMsg, strUserId, strPassword, strCZLX, strAddress)
                End With
            Catch ex As Exception
                doWriteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 清除“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doDeleteXitongJinchuRizhi = .doDeleteXitongJinchuRizhi(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doDeleteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     intXH                ：要删除的序号
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal intXH As Integer) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doDeleteXitongJinchuRizhi = .doDeleteXitongJinchuRizhi(strErrMsg, strUserId, strPassword, intXH)
                End With
            Catch ex As Exception
                doDeleteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strQSRQ              ：要删除的开始日期
        '     strZZRQ              ：要删除的结束日期
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strQSRQ As String, _
            ByVal strZZRQ As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doDeleteXitongJinchuRizhi = .doDeleteXitongJinchuRizhi(strErrMsg, strUserId, strPassword, strQSRQ, strZZRQ)
                End With
            Catch ex As Exception
                doDeleteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 写“在线用户”数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doWriteZaixianYonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doWriteZaixianYonghu = .doWriteZaixianYonghu(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doWriteZaixianYonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 删除“在线用户”数据
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doDeleteZaixianYonghu( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doDeleteZaixianYonghu = .doDeleteZaixianYonghu(strErrMsg, strUserId, strPassword)
                End With
            Catch ex As Exception
                doDeleteZaixianYonghu = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 获取用户操作日志数据
        '     strErrMsg                ：如果错误，则返回错误信息
        '     strUserId                ：用户标识
        '     strPassword              ：用户密码
        '     strWhere                 ：搜索条件
        '     objLogData               ：(返回)数据集
        ' 返回
        '     True                     ：成功
        '     False                    ：失败
        '----------------------------------------------------------------
        Public Function getYonghuCaozuoRizhiData( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strWhere As String, _
            ByRef objLogData As Xydc.Platform.Common.Data.CustomerData) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    getYonghuCaozuoRizhiData = .getYonghuCaozuoRizhiData(strErrMsg, strUserId, strPassword, strWhere, objLogData)
                End With
            Catch ex As Exception
                getYonghuCaozuoRizhiData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 写“用户操作日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strAddress           ：机器地址
        '     strCZSM              ：操作说明
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        '----------------------------------------------------------------
        Public Function doWriteYonghuCaozuoRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strCZSM As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doWriteYonghuCaozuoRizhi = .doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strCZSM)
                End With
            Catch ex As Exception
                doWriteYonghuCaozuoRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 写“系统进出日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strCZLX              ：操作类型
        '     strAddress           ：机器地址
        '     strMachine           ：机器名称
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改说明：
        '      增加strMachine参数及相关处理
        '----------------------------------------------------------------
        Public Function doWriteXitongJinchuRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strCZLX As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doWriteXitongJinchuRizhi = .doWriteXitongJinchuRizhi(strErrMsg, strUserId, strPassword, strCZLX, strAddress, strMachine)
                End With
            Catch ex As Exception
                doWriteXitongJinchuRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 写“用户操作日志”
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strAddress           ：机器地址
        '     strMachine           ：机器名称
        '     strCZSM              ：操作说明
        ' 返回
        '     True                 ：成功
        '     False                ：失败
        ' 更改说明：
        '      增加strMachine参数及相关处理
        '----------------------------------------------------------------
        Public Function doWriteYonghuCaozuoRizhi( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByVal strPassword As String, _
            ByVal strAddress As String, _
            ByVal strMachine As String, _
            ByVal strCZSM As String) As Boolean

            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doWriteYonghuCaozuoRizhi = .doWriteYonghuCaozuoRizhi(strErrMsg, strUserId, strPassword, strAddress, strMachine, strCZSM)
                End With
            Catch ex As Exception
                doWriteYonghuCaozuoRizhi = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 检查“公共_B_人员”的标识是否已存在
        '     strErrMsg            ：如果错误，则返回错误信息
        '     strUserId            ：用户标识
        '     strPassword          ：用户密码
        '     strNewUserId         ：检查的用户标识
        '     strNewUserZZDM       ：检查的用户组织代码
        ' 返回
        '     intType              ：1-同部门添加，0-不同部门添加
        '     objCustomerData      ：如果存在，就返回存在的纪录集
        '     True                 ：不存在
        '     False                ：存在

        '----------------------------------------------------------------
        Public Function doVerifyRenyuanData( _
           ByRef strErrMsg As String, _
           ByVal strUserId As String, _
           ByVal strPassword As String, _
           ByVal strNewUserId As String, _
           ByVal strNewUserZZDM As String, _
           ByRef intType As Integer, _
           ByRef objCustomerData As Xydc.Platform.Common.Data.CustomerData) As Boolean
            Try
                With New Xydc.Platform.DataAccess.dacCustomer
                    doVerifyRenyuanData = .doVerifyRenyuanData(strErrMsg, strUserId, strPassword, strNewUserId, strNewUserZZDM, intType, objCustomerData)
                End With
            Catch ex As Exception
                doVerifyRenyuanData = False
                strErrMsg = ex.Message
            End Try
        End Function

    End Class 'rulesCustomer

End Namespace 'Xydc.Platform.BusinessRules
