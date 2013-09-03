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
Imports System.IO
Imports System.Xml
Imports System.Web
Imports System.Web.UI
Imports System.Security
Imports System.ComponentModel
Imports System.Data
Imports Microsoft.VisualBasic

Imports Xydc.Platform.Common
Imports Xydc.Platform.Common.Data
Imports Xydc.Platform.SystemFramework

Namespace Xydc.Platform.web

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.web
    ' ����    ��PageBase
    '
    ' ���������� 
    '   ������ҳ��ĸ���
    '----------------------------------------------------------------
    Public Class PageBase
        Inherits System.Web.UI.Page

        '
        ' Exception Logging constant
        '
        Private Const UNHANDLED_EXCEPTION As String = "Unhandled Exception:"

        '
        ' Session Key Constants
        '
        Private Const KEY_CACHECUSTOMER_DATASET As String = "Cache:Customer:DataSet"
        Private Const KEY_CACHECUSTOMER_USERID As String = "Cache:Customer:UserId"
        Private Const KEY_CACHECUSTOMER_USERPWD As String = "Cache:Customer:UserPwd"
        Private Const KEY_CACHECUSTOMER_USERORGPWD As String = "Cache:Customer:UserOrgPwd"
        Private Const KEY_CACHECUSTOMER_ENTERTIME As String = "Cache:Customer:EnterTime"
        Private Const KEY_CACHECUSTOMER_APPLOCKED As String = "Cache:Customer:AppLocked"
        Private Const KEY_CACHECUSTOMER_FULLSCREEN As String = "Cache:Customer:FullScreen"
         Private Const KEY_CACHECUSTOMER_LASTSCANTIME_CHAT As String = "Cache:Customer:LastScanTime:Chat"
        Private Const KEY_CACHECUSTOMER_LASTSCANTIME_NOTICE As String = "Cache:Customer:LastScanTime:Notice"


        Private Shared ReadOnly Property UrlSuffix() As String

            Get
                UrlSuffix = HttpContext.Current.Request.Url.Host + HttpContext.Current.Request.ApplicationPath
            End Get

        End Property


        '----------------------------------------------------------------
        ' Property SecureUrlBase:
        '   Retrieves the Prefix for URLs in the Secure directory.
        '----------------------------------------------------------------
        Public Shared ReadOnly Property SecureUrlBase() As String

            Get
                If jsoaConfiguration.EnableSsl Then
                    SecureUrlBase = "https://"
                Else
                    SecureUrlBase = "http://"
                End If
                SecureUrlBase = SecureUrlBase + UrlSuffix
            End Get

        End Property

        '----------------------------------------------------------------
        ' Property UrlHost:
        '   Retrieves the Prefix for URLs.
        '----------------------------------------------------------------
        Public Shared ReadOnly Property UrlHost() As String

            Get
                UrlHost = "http://" + HttpContext.Current.Request.Url.Host
            End Get

        End Property

        '----------------------------------------------------------------
        ' Property UrlBase:
        '   Retrieves the Prefix for URLs.
        '----------------------------------------------------------------
        Public Shared ReadOnly Property UrlBase() As String

            Get
                UrlBase = "http://" + UrlSuffix
            End Get

        End Property

        '----------------------------------------------------------------
        ' ��¼�û���Ϣ���ݼ�
        '----------------------------------------------------------------
        Public Property Customer() As System.Data.DataSet

            Get
                Try
                    Customer = CType(Session.Item(KEY_CACHECUSTOMER_DATASET), System.Data.DataSet)
                Catch
                    Customer = Nothing
                End Try
            End Get

            Set(ByVal Value As System.Data.DataSet)
                If Value Is Nothing Then
                    Dim objDataSet As System.Data.DataSet = Nothing
                    Try
                        objDataSet = CType(Session.Item(KEY_CACHECUSTOMER_DATASET), System.Data.DataSet)
                    Catch
                        objDataSet = Nothing
                    End Try
                    Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
                    Session.Remove(KEY_CACHECUSTOMER_DATASET)
                Else
                    Session.Item(KEY_CACHECUSTOMER_DATASET) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' ��¼�û���ID
        '----------------------------------------------------------------
        Public Property UserId() As String

            Get
                Try
                    UserId = CType(Session.Item(KEY_CACHECUSTOMER_USERID), String)
                Catch
                    UserId = ""
                End Try
                If UserId Is Nothing Then UserId = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_USERID)
                Else
                    Session.Item(KEY_CACHECUSTOMER_USERID) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' ��¼�û�������(ԭʼ����)
        '----------------------------------------------------------------
        Public Property UserOrgPassword() As String

            Get
                Try
                    UserOrgPassword = CType(Session.Item(KEY_CACHECUSTOMER_USERORGPWD), String)
                Catch
                    UserOrgPassword = ""
                End Try
                If UserOrgPassword Is Nothing Then UserOrgPassword = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_USERORGPWD)
                Else
                    Session.Item(KEY_CACHECUSTOMER_USERORGPWD) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' ��¼�û�������(��֤�������)
        '----------------------------------------------------------------
        Public Property UserPassword() As String

            Get
                Try
                    UserPassword = CType(Session.Item(KEY_CACHECUSTOMER_USERPWD), String)
                Catch
                    UserPassword = ""
                End Try
                If UserPassword Is Nothing Then UserPassword = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_USERPWD)
                Else
                    Session.Item(KEY_CACHECUSTOMER_USERPWD) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' ��¼�û��Ľ���ʱ��
        '----------------------------------------------------------------
        Public Property UserEnterTime() As String

            Get
                Try
                    UserEnterTime = CType(Session.Item(KEY_CACHECUSTOMER_ENTERTIME), String)
                Catch
                    UserEnterTime = ""
                End Try
                If UserEnterTime Is Nothing Then UserEnterTime = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_ENTERTIME)
                Else
                    Session.Item(KEY_CACHECUSTOMER_ENTERTIME) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' ��¼�û���֪ͨ��Ϣ��ʱ������ϴμ��ʱ��
        '----------------------------------------------------------------
        Public Property LastScanTime_Notice() As String

            Get
                Try
                    LastScanTime_Notice = CType(Session.Item(KEY_CACHECUSTOMER_LASTSCANTIME_NOTICE), String)
                Catch
                    LastScanTime_Notice = ""
                End Try
                If LastScanTime_Notice Is Nothing Then LastScanTime_Notice = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_LASTSCANTIME_NOTICE)
                Else
                    Session.Item(KEY_CACHECUSTOMER_LASTSCANTIME_NOTICE) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' ��¼�û��ļ�ʱ������ʱ������ϴμ��ʱ��
        '----------------------------------------------------------------
        Public Property LastScanTime_Chat() As String

            Get
                Try
                    LastScanTime_Chat = CType(Session.Item(KEY_CACHECUSTOMER_LASTSCANTIME_CHAT), String)
                Catch
                    LastScanTime_Chat = ""
                End Try
                If LastScanTime_Chat Is Nothing Then LastScanTime_Chat = ""
            End Get

            Set(ByVal Value As String)
                If Value = "" Then
                    Session.Remove(KEY_CACHECUSTOMER_LASTSCANTIME_CHAT)
                Else
                    Session.Item(KEY_CACHECUSTOMER_LASTSCANTIME_CHAT) = Value
                End If
            End Set

        End Property

        '----------------------------------------------------------------
        ' ��¼�û�����
        '----------------------------------------------------------------
        Public ReadOnly Property UserXM() As String

            Get
                Try
                    If Not (Me.Customer Is Nothing) Then
                        If Not (Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN) Is Nothing) Then
                            With Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count > 0 Then
                                    UserXM = CType(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYMC), String)
                                Else
                                    UserXM = ""
                                End If
                            End With
                        End If
                    End If
                Catch
                    UserXM = ""
                End Try
                If UserXM Is Nothing Then UserXM = ""
                UserXM = UserXM.Trim
            End Get

        End Property

        '----------------------------------------------------------------
        ' ��¼�û���λ����
        '----------------------------------------------------------------
        Public ReadOnly Property UserBmdm() As String

            Get
                Try
                    If Not (Me.Customer Is Nothing) Then
                        If Not (Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN) Is Nothing) Then
                            With Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count > 0 Then
                                    UserBmdm = CType(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_ZZDM), String)
                                Else
                                    UserBmdm = ""
                                End If
                            End With
                        End If
                    End If
                Catch
                    UserBmdm = ""
                End Try
                If UserBmdm Is Nothing Then UserBmdm = ""
                UserBmdm = UserBmdm.Trim
            End Get

        End Property

        '----------------------------------------------------------------
        ' ��¼�û���λ����
        '----------------------------------------------------------------
        Public ReadOnly Property UserBmmc() As String

            Get
                Try
                    If Not (Me.Customer Is Nothing) Then
                        If Not (Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN) Is Nothing) Then
                            With Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count > 0 Then
                                    UserBmmc = CType(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_ZUZHIJIGOU_ZZMC), String)
                                Else
                                    UserBmmc = ""
                                End If
                            End With
                        End If
                    End If
                Catch
                    UserBmmc = ""
                End Try
                If UserBmmc Is Nothing Then UserBmmc = ""
                UserBmmc = UserBmmc.Trim
            End Get

        End Property


        '----------------------------------------------------------------
        ' �û��Ƿ�Ӧ��������
        '----------------------------------------------------------------
        Public Property AppLocked() As Boolean

            Get
                Try
                    AppLocked = CType(Session.Item(KEY_CACHECUSTOMER_APPLOCKED), Boolean)
                Catch
                    AppLocked = False
                End Try
            End Get

            Set(ByVal Value As Boolean)
                If Value = False Then
                    Session.Remove(KEY_CACHECUSTOMER_APPLOCKED)
                Else
                    Session.Item(KEY_CACHECUSTOMER_APPLOCKED) = Value
                End If
            End Set

        End Property
       
        '----------------------------------------------------------------
        ' ���ڵ�ȫ����������ʾ״̬
        '----------------------------------------------------------------
        Public Property FullScreen() As Boolean

            Get
                Try
                    FullScreen = CType(Session.Item(KEY_CACHECUSTOMER_FULLSCREEN), Boolean)
                Catch
                    FullScreen = False
                End Try
            End Get

            Set(ByVal Value As Boolean)
                If Value = False Then
                    Session.Remove(KEY_CACHECUSTOMER_FULLSCREEN)
                Else
                    Session.Item(KEY_CACHECUSTOMER_FULLSCREEN) = Value
                End If
            End Set

        End Property










        '----------------------------------------------------------------
        ' Sub Page_Error:
        '   Handles errors that may be encountered when displaying this page.
        '----------------------------------------------------------------
        Protected Overrides Sub OnError(ByVal e As EventArgs)

            'ApplicationLog.WriteError(ApplicationLog.FormatException(Server.GetLastError(), UNHANDLED_EXCEPTION))
            MyBase.OnError(e)

        End Sub

        '----------------------------------------------------------------
        '������볤�ȣ����������Ҫ����ǿ�Ƶ��޸�����Url
        '���أ�
        '    True  - ���ټ���ִ�е�ǰҳ�����
        '    False - ����ִ�е�ǰҳ�����
        '----------------------------------------------------------------
        Public Function doCheckPassword() As Boolean

            Dim strUrl As String

            doCheckPassword = False
            Try
                If Xydc.Platform.Common.jsoaConfiguration.CheckPassword = True Then
                    'If Me.UserOrgPassword.Length < Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength Then
                    '    strUrl = Me.UrlBase + "/secure/modifypwd.aspx"
                    '    doCheckPassword = True
                    '    Response.Redirect(strUrl)
                    'End If
                    If Me.doValidPassword(Me.UserOrgPassword) = False Then
                        strUrl = Me.UrlBase + "/secure/modifypwd.aspx"
                        doCheckPassword = True
                        Response.Redirect(strUrl)
                    End If
                End If
            Catch ex As Exception
            End Try

            Exit Function

        End Function

        '----------------------------------------------------------------
        '������볤���Ƿ���ϳ��Ⱥ�ǿ��Ҫ��
        '���룺
        '    strPassword��Ҫ��������
        '���أ�
        '    True  - ����
        '    False - ������
        '�޸ļ�¼��
        '----------------------------------------------------------------
        Public Function doValidPassword(ByVal strPassword As String) As Boolean

            Dim intLevel As Integer = 0

            doValidPassword = False
            Try
                strPassword = Me.UserOrgPassword

                If Xydc.Platform.Common.jsoaConfiguration.CheckPassword = True Then
                    If strPassword.Length < Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength Then
                        '�����ϳ���Ҫ��
                        Exit Function
                    End If

                    '����ǿ�ȼ��
                    Dim blnFoundSign As Boolean = False
                    Dim blnFoundLCap As Boolean = False
                    Dim blnFoundUCap As Boolean = False
                    Dim blnFoundNum As Boolean = False
                    Dim objBytes() As Char

                    objBytes = strPassword.ToCharArray()
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = objBytes.Length
                    For i = 0 To intCount - 1 Step 1
                        If Char.IsDigit(objBytes(i)) = True Then
                            blnFoundNum = True
                        End If
                        If Char.IsLetter(objBytes(i)) = True And Char.IsLower(objBytes(i)) = True Then
                            blnFoundLCap = True
                        End If
                        If Char.IsLetter(objBytes(i)) = True And Char.IsUpper(objBytes(i)) = True Then
                            blnFoundUCap = True
                        End If
                        If Char.IsPunctuation(objBytes(i)) = True Then
                            blnFoundSign = True
                        End If
                    Next
                    If blnFoundNum = True Then
                        intLevel += 1
                    End If
                    If blnFoundLCap = True Then
                        intLevel += 1
                    End If
                    If blnFoundUCap = True Then
                        intLevel += 1
                    End If
                    If blnFoundSign = True Then
                        intLevel += 1
                    End If
                    If intLevel < Xydc.Platform.Common.jsoaConfiguration.PasswordLevel Then
                        '������ǿ��Ҫ��
                        Exit Function
                    End If
                End If
            Catch ex As Exception
                Exit Function
            End Try

            doValidPassword = True
            Exit Function

        End Function

        '----------------------------------------------------------------
        '���û�������¼��DataSetд�뵽XML�ļ���
        '    strErrMsg   �����ش�����Ϣ
        '    objDataSet  ��Ҫд�����ݼ�
        '    strXmlFile  ����д���XML�ļ��������·��
        '���أ�
        '    True        ���ɹ�
        '    False       ��ʧ��
        '----------------------------------------------------------------
        Private Function doWriteXml( _
            ByRef strErrMsg As String, _
            ByVal objDataSet As System.Data.DataSet, _
            ByVal strXmlFile As String) As Boolean

            doWriteXml = False
            strErrMsg = ""

            Try
                '���
                If objDataSet Is Nothing Then
                    Exit Try
                End If
                If strXmlFile Is Nothing Then strXmlFile = ""
                strXmlFile = strXmlFile.Trim
                If strXmlFile = "" Then
                    Exit Try
                End If

                '����
                objDataSet.WriteXml(strXmlFile, System.Data.XmlWriteMode.WriteSchema)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doWriteXml = True
            Exit Function

errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        '�����û�
        '    strErrMsg   �����ش�����Ϣ
        '    strUserId   ���û���ʶ
        '���أ�
        '    True        ���ɹ�
        '    False       ��ʧ��
        '----------------------------------------------------------------
        Public Function doLockAccount( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String) As Boolean

            Dim strXmlFile As String = Xydc.Platform.Common.jsoaConfiguration.AccountLockDataFile
            Dim strField_LockTime As String = "locktime"
            Dim strField_Valid As String = "valid"
            Dim strField_Name As String = "name"

            Dim objDataSet As System.Data.DataSet

            doLockAccount = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                strXmlFile = Server.MapPath(Request.ApplicationPath + strXmlFile)

                '��ȡ�û���������
                objDataSet = New System.Data.DataSet
                objDataSet.ReadXmlSchema(strXmlFile)
                objDataSet.ReadXml(strXmlFile)

                '���ڣ�
                Dim strFilter As String = strField_Name + " = '" + strUserId + "'"
                Dim blnFound As Boolean = False
                With objDataSet.Tables(0)
                    .DefaultView.RowFilter = strFilter
                    If .DefaultView.Count > 0 Then
                        blnFound = True
                    Else
                        .DefaultView.RowFilter = ""
                    End If
                End With

                '����
                Dim objDataRow As System.Data.DataRow
                If blnFound = False Then
                    With objDataSet.Tables(0)
                        objDataRow = .NewRow

                        objDataRow.Item(strField_Name) = strUserId
                        objDataRow.Item(strField_LockTime) = Now.ToString("yyyy-MM-dd HH:mm:ss")
                        objDataRow.Item(strField_Valid) = CType(1, Integer)

                        .Rows.Add(objDataRow)
                    End With
                Else
                    With objDataSet.Tables(0)
                        objDataRow = .DefaultView.Item(0).Row

                        objDataRow.Item(strField_Name) = strUserId
                        objDataRow.Item(strField_LockTime) = Now.ToString("yyyy-MM-dd HH:mm:ss")
                        objDataRow.Item(strField_Valid) = CType(1, Integer)

                        .DefaultView.RowFilter = ""
                    End With
                End If

                '����
                If Me.doWriteXml(strErrMsg, objDataSet, strXmlFile) = False Then
                    GoTo errProc
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            doLockAccount = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        '�����û�
        '    strErrMsg   �����ش�����Ϣ
        '    strUserId   ���û���ʶ
        '���أ�
        '    True        ���ɹ�
        '    False       ��ʧ��
        '----------------------------------------------------------------
        Public Function doUnlockAccount( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String) As Boolean

            Dim strXmlFile As String = Xydc.Platform.Common.jsoaConfiguration.AccountLockDataFile
            Dim strField_LockTime As String = "locktime"
            Dim strField_Valid As String = "valid"
            Dim strField_Name As String = "name"

            Dim objDataSet As System.Data.DataSet

            doUnlockAccount = False
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                strXmlFile = Server.MapPath(Request.ApplicationPath + strXmlFile)

                '��ȡ�û���������
                objDataSet = New System.Data.DataSet
                objDataSet.ReadXmlSchema(strXmlFile)
                objDataSet.ReadXml(strXmlFile)

                'ʧЧ����
                Dim blnChanged As Boolean = False
                Dim intCount As Integer
                Dim i As Integer
                With objDataSet.Tables(0)
                    .DefaultView.RowFilter = strField_Name + " = '" + strUserId + "'"
                    intCount = .DefaultView.Count
                    For i = 0 To intCount - 1 Step 1
                        .DefaultView.Item(i).Item(strField_Valid) = CType(0, Integer)
                        blnChanged = True
                    Next
                    .DefaultView.RowFilter = ""
                End With

                '����
                If blnChanged = True Then
                    If Me.doWriteXml(strErrMsg, objDataSet, strXmlFile) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            doUnlockAccount = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        '----------------------------------------------------------------
        '�ж��û��Ƿ�������
        '    strErrMsg   �����ش�����Ϣ
        '    strUserId   ���û���ʶ
        '    blnLocked   ������True/False
        '    strLockTime �����ؿ�ʼ����ʱ��
        '���أ�
        '    True        ���ɹ�
        '    False       ��ʧ��
        '----------------------------------------------------------------
        Public Function isAccountLocked( _
            ByRef strErrMsg As String, _
            ByVal strUserId As String, _
            ByRef blnLocked As Boolean, _
            ByRef strLockTime As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim objDataSet As System.Data.DataSet

            Dim strXmlFile As String = Xydc.Platform.Common.jsoaConfiguration.AccountLockDataFile
            Dim strField_LockTime As String = "locktime"
            Dim strField_Valid As String = "valid"
            Dim strField_Name As String = "name"

            isAccountLocked = False
            blnLocked = False
            strLockTime = ""
            strErrMsg = ""

            Try
                '���
                If strUserId Is Nothing Then strUserId = ""
                strUserId = strUserId.Trim
                If strUserId = "" Then
                    Exit Try
                End If
                strXmlFile = Server.MapPath(Request.ApplicationPath + strXmlFile)

                '��ȡ�û���������
                objDataSet = New System.Data.DataSet
                objDataSet.ReadXmlSchema(strXmlFile)
                objDataSet.ReadXml(strXmlFile)

                '��������
                Dim strFilter As String
                strFilter = strField_Name + " = '" + strUserId + "' and " + strField_Valid + " = 1"
                objDataSet.Tables(0).DefaultView.RowFilter = strFilter

                '����
                Dim strTime As String
                If objDataSet.Tables(0).DefaultView.Count > 0 Then
                    '�Ƿ񳬹�����ʱ�䣿
                    With objDataSet.Tables(0).DefaultView
                        strTime = objPulicParameters.getObjectValue(.Item(0).Item(strField_LockTime), "")
                    End With
                    If objPulicParameters.isDatetimeString(strTime) = True Then
                        Dim objTime As System.DateTime
                        objTime = CType(strTime, System.DateTime)
                        objTime = objTime.AddMinutes(Xydc.Platform.Common.jsoaConfiguration.DeadAccountLock)
                        If objTime > Now Then
                            '�Դ�������
                            strLockTime = strTime
                            blnLocked = True
                            Exit Try
                        End If
                    End If

                    '�������(����Ϊ��Чvalid=0)
                    If Me.doUnlockAccount(strErrMsg, strUserId) = False Then
                        GoTo errProc
                    End If
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)

            isAccountLocked = True
            Exit Function

errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDataSet)
            Exit Function

        End Function

        'Private Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '    ''�����б��ػ���
        '    Try
        '        'set cache-control = no-cache
        '        Response.CacheControl = "No-Cache"
        '        'set Pragma = no-cache
        '        Response.AddHeader("Pragma", "No-Cache")
        '        'set Expires = -1
        '        Response.Expires = -1
        '    Catch ex As Exception
        '    End Try

        'End Sub


        '----------------------------------------------------------------
        ' ��¼�û�����
        '----------------------------------------------------------------
        Public ReadOnly Property UserZM() As String

            Get
                Try
                    If Not (Me.Customer Is Nothing) Then
                        If Not (Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN) Is Nothing) Then
                            With Me.Customer.Tables(Xydc.Platform.Common.Data.CustomerData.TABLE_GG_B_RENYUAN_FULLJOIN)
                                If .Rows.Count > 0 Then
                                    UserZM = CType(.Rows(0).Item(Xydc.Platform.Common.Data.CustomerData.FIELD_GG_B_RENYUAN_RYZM), String)
                                Else
                                    UserZM = ""
                                End If
                            End With
                        End If
                    End If
                Catch
                    UserZM = ""
                End Try
                If UserZM Is Nothing Then UserZM = ""
                If UserZM = "" Then UserZM = UserXM
                UserZM = UserZM.Trim
            End Get

        End Property
       
        '----------------------------------------------------------------
        'PageԤ����-ִ�е�¼����������Ȱ�ȫ��Ļ�������
        '���룺
        '    blnCheckPassword - True�������,False���������
        '    blnSaveAccessLog - True��¼������־,False����¼������־
        '���أ�
        '    True  - ���ټ���ִ�е�ǰҳ�����
        '    False - ����ִ�е�ǰҳ�����
        '----------------------------------------------------------------
        Public Function doPagePreprocess( _
            ByVal blnCheckPassword As Boolean, _
            ByVal blnSaveAccessLog As Boolean) As Boolean

            Dim strUrl As String

            doPagePreprocess = False
            Try
                '����¼ƾ֤��

                If Me.Customer Is Nothing Then
                    'û�е�¼�����򵽵�¼ҳ��


                    '���ƾ֤ - ǿ��Ҫ�����µ�¼��
                    System.Web.Security.FormsAuthentication.SignOut()
                    Me.Customer = Nothing
                    Me.UserId = ""
                    Me.UserPassword = ""

                    '���·��ʱ�ҳ
                    strUrl = Request.Url.PathAndQuery
                    doPagePreprocess = True
                    Response.Redirect(strUrl)
                    Exit Function
                Else
                    '�ѵ�¼��������Ҫ��֤����Ҫ��
                    If blnCheckPassword = True Then
                        If Xydc.Platform.Common.jsoaConfiguration.CheckPassword = True Then

                            'If Me.UserOrgPassword.Length < Xydc.Platform.Common.jsoaConfiguration.MinPasswordLength Then
                            '    strUrl = Me.UrlBase + "/secure/modifypwd.aspx"
                            '    doPagePreprocess = True
                            '    Response.Redirect(strUrl)
                            '    Exit Function
                            'End If
                            If Me.doValidPassword(Me.UserOrgPassword) = False Then
                                strUrl = Me.UrlBase + "/secure/modifypwd.aspx"
                                doPagePreprocess = True
                                Response.Redirect(strUrl)
                                Exit Function
                            End If

                        End If
                    End If

                    '�Ƿ��¼������־
                    If blnSaveAccessLog = True Then
                        Xydc.Platform.SystemFramework.ApplicationLog.WriteInfo(Request.UserHostAddress, Request.UserHostName, "[" + Me.UserId + "]������[" + Request.Url.AbsoluteUri + "]��")
                    End If
                End If
            Catch ex As Exception
                '���Դ���
            End Try

            Exit Function

        End Function


    End Class

End Namespace
