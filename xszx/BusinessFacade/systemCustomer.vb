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
Imports System.Security.Cryptography
Imports Microsoft.VisualBasic

Imports Xydc.Platform.SystemFramework
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.BusinessRules

Namespace Xydc.Platform.BusinessFacade
    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：systemCustomer
    '
    ' 功能描述： 
    '   　提供对用户信息处理的表现层支持
    '----------------------------------------------------------------
    Public Class systemCustomer
        Inherits MarshalByRefObject

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.systemCustomer)
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
        ' 不成功则返回错误。
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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doVerifyUserPassword = .doVerifyUserPassword(strErrMsg, strUserId, strPassword, strNewPassword)
                End With
            Catch ex As Exception
                doVerifyUserPassword = False
                strErrMsg = ex.Message
            End Try

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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doModifyPassword = .doModifyPassword(strErrMsg, strCzyId, strCzyPassword, strUserId, strNewPassword1, strNewPassword2, strNewPassword)
                End With
            Catch ex As Exception
                doModifyPassword = False
                strErrMsg = ex.Message
            End Try

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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRenyuanInBumenData = .getRenyuanInBumenData(strErrMsg, strUserId, strPassword, strZZDM, blnBaohanXiaji, strWhere, objRenyuanData)
                End With
            Catch ex As Exception
                getRenyuanInBumenData = False
                strErrMsg = ex.Message
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据指定范围名称获取范围下的组织信息或人员信息
        ' 含人员的全部连接数据
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRydmByRymc = .getRydmByRymc(strErrMsg, strUserId, strPassword, strRYMC, strRYDM)
                End With
            Catch ex As Exception
                getRydmByRymc = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getRymcByRydm = .getRymcByRydm(strErrMsg, strUserId, strPassword, strRYDM, strRYMC)
                End With
            Catch ex As Exception
                getRymcByRydm = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzdmByZzmc = .getZzdmByZzmc(strErrMsg, strUserId, strPassword, strZZMC, strZZDM)
                End With
            Catch ex As Exception
                getZzdmByZzmc = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzbmByZzmc = .getZzbmByZzmc(strErrMsg, strUserId, strPassword, strZZMC, strZZBM)
                End With
            Catch ex As Exception
                getZzbmByZzmc = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzmcByZzdm = .getZzmcByZzdm(strErrMsg, strUserId, strPassword, strZZDM, strZZMC)
                End With
            Catch ex As Exception
                getZzmcByZzdm = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzmcByZzbm = .getZzmcByZzbm(strErrMsg, strUserId, strPassword, strZZDM, strBMXX)
                End With
            Catch ex As Exception
                getZzmcByZzbm = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzmcByRydm = .getZzmcByRydm(strErrMsg, strUserId, strPassword, strRYDM, strZZMC)
                End With
            Catch ex As Exception
                getZzmcByRydm = False
                strErrMsg = ex.Message
            End Try

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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    getZzmcByRymc = .getZzmcByRymc(strErrMsg, strUserId, strPassword, strRYMC, strZZMC)
                End With
            Catch ex As Exception
                getZzmcByRymc = False
                strErrMsg = ex.Message
            End Try

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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doSaveRenyuanData = .doSaveRenyuanData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objenumEditType, objNewDataSG)
                End With
            Catch ex As Exception
                doSaveRenyuanData = False
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

            Try
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doSaveRenyuanData = .doSaveRenyuanData(strErrMsg, strUserId, strPassword, objOldData, objNewData, objUpdateData, objenumEditType, objNewDataSG)
                End With
            Catch ex As Exception
                doSaveRenyuanData = False
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
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
                With New Xydc.Platform.BusinessRules.rulesCustomer
                    doVerifyRenyuanData = .doVerifyRenyuanData(strErrMsg, strUserId, strPassword, strNewUserId, strNewUserZZDM, intType, objCustomerData)
                End With
            Catch ex As Exception
                doVerifyRenyuanData = False
                strErrMsg = ex.Message
            End Try

        End Function
    End Class

End Namespace
