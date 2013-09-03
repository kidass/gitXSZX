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

Imports Microsoft.VisualBasic

Imports System
Imports System.Data
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Utilities

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.Common.Utilities
    ' ����    ��PulicParameters
    '
    ' ����������
    '     ����ϵͳ�õ�ȫ�ֲ�����Ϣ
    '----------------------------------------------------------------
    Public Class PulicParameters
        Implements IDisposable

        '�������ݼ����õķǶԳ���Կ��
        Private m_bKey() As Byte = {&H15, &H28, &H34, &H41, &H85, &H36, &H77, &H28, &H19, &H10, &H51, &H62, &H93, &H64, &H37, &H52}
        Private m_bIV() As Byte = {&H61, &H22, &H53, &H74, &H25, &H66, &H97, &H98, &H29, &H55, &H46, &H73, &H19, &H43, &H77, &H31}

        '������������enum
        Public Enum enumSortType
            Asc = 1
            Desc = 2
            None = 3
        End Enum

        '����༭ģʽ
        Public Enum enumEditType
            eSelect = 0
            eAddNew = 1
            eUpdate = 2
            eDelete = 3
            eCpyNew = 4
        End Enum

        '��������
        Public Const SearchConfig As Boolean = True

        '��ֵ�ָ���
        Public Const CharSeparate As String = ","
        '��ʾTrue���ַ�
        Public Const CharTrue As String = "��"
        '��ʾFalse���ַ�
        Public Const CharFalse As String = "��"
        '������ָ����ʾ��
        Public Const CharPointer As String = "��"
        '�ַ������ͷ
        Public Const CharArrow As String = "��"
        '���������ַ�
        Public Const CharAsc As String = "��"
        '���������ַ�
        Public Const CharDesc As String = "��"
        '�ּ�����ָ���
        Public Const CharFjdmSeparate As String = "."

        '�ļ��ֺ�������
        Public Const CharWjzhLf As String = "��"
        '�ļ��ֺ�������
        Public Const CharWjzhRt As String = "��"

        '�ĵ���������
        Public Const FileProtectPassword As String = "12345678"

        'Request�����е�CheckBoxѡ��״ֵ̬
        Public Const CheckBoxCheckedValue As String = "on"

        'ģ��֮�����ʱQueryString���õ���SessionId����
        '����ģ����������õ�SessionId
        Public Const ISessionId As String = "iSessionId"
        '���巵�ص�����ģ��ʱ��SessionId
        Public Const OSessionId As String = "oSessionId"
        '���屣��ģ���������л����õ�SessionId
        Public Const MSessionId As String = "mSessionId"

        '�����ļ���ȱʡ��׺
        Public Const BACKUPFILEEXT As String = ".bak"









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements IDisposable.Dispose
            Dispose(True)
            GC.SuppressFinalize(True)
        End Sub

        '----------------------------------------------------------------
        ' ��������ʵ��
        '----------------------------------------------------------------
        Protected Overridable Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Utilities.PulicParameters)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' ����GUID
        '----------------------------------------------------------------
        Public Function getNewGuid() As String

            Try
                getNewGuid = System.Guid.NewGuid().ToString()
                getNewGuid = getNewGuid.ToUpper()
            Catch ex As Exception
                getNewGuid = ""
            End Try

        End Function










        '----------------------------------------------------------------
        ' ��ȡUnicode���ַ���ת��ΪMBCS�ַ������ֽڳ���
        '----------------------------------------------------------------
        Public Function getStringLength(ByVal strValue As String) As Integer

            Try
                Dim bSrc() As Byte
                bSrc = System.Text.Encoding.Unicode.GetBytes(strValue)
                Dim bDes() As Byte
                bDes = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.GetEncoding("GB2312"), bSrc)
                getStringLength = bDes.Length()
            Catch ex As Exception
                getStringLength = strValue.Length
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��Unicode���ַ����л�ȡָ�����ȵ��ַ��������Ȱ�MBCS����
        '----------------------------------------------------------------
        Public Function getSubString(ByVal strValue As String, ByVal intLen As Integer) As String

            Try
                '����MBCS�ֽ�����
                Dim bSrc() As Byte
                bSrc = System.Text.Encoding.Unicode.GetBytes(strValue)
                Dim bDes() As Byte
                bDes = System.Text.Encoding.Convert(System.Text.Encoding.Unicode, System.Text.Encoding.GetEncoding("GB2312"), bSrc)
                '��intLen֮�����
                If bDes.Length > intLen Then
                    Dim bTmp(intLen - 1) As Byte
                    Dim i As Integer
                    For i = 0 To intLen - 1 Step 1
                        '����Ƿ�Ϊ˫�ֽڣ�
                        If i = intLen - 1 Then
                            If bDes(i) >= 128 Then
                                bTmp(i) = 0
                            Else
                                bTmp(i) = bDes(i)
                            End If
                        Else
                            bTmp(i) = bDes(i)
                        End If
                    Next
                    Dim bFin() As Byte
                    bFin = System.Text.Encoding.Convert(System.Text.Encoding.GetEncoding("GB2312"), System.Text.Encoding.Unicode, bTmp)
                    getSubString = System.Text.Encoding.Unicode.GetString(bFin)
                Else
                    getSubString = strValue
                End If
            Catch ex As Exception
                getSubString = strValue
            End Try

        End Function









        '----------------------------------------------------------------
        ' �Ƿ�ΪInteger
        '----------------------------------------------------------------
        Public Function isIntegerString(ByVal strValue As String) As Boolean

            Dim intValue As Integer
            isIntegerString = False
            Try
                intValue = CType(strValue, Integer)
                isIntegerString = True
            Catch ex As Exception
                isIntegerString = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊȫ�����ַ���
        '----------------------------------------------------------------
        Public Function isNumericString(ByVal strValue As String) As Boolean

            Dim dblValue As Double
            isNumericString = False
            Try
                dblValue = CType(strValue, Double)
                If strValue.IndexOf(",") > 0 Then
                    Exit Try
                End If
                If strValue.IndexOf(".") > 0 Then
                    Exit Try
                End If
                isNumericString = True
            Catch ex As Exception
                isNumericString = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ������ֵ
        '----------------------------------------------------------------
        Public Function isFloatString(ByVal strValue As String) As Boolean

            Dim dblValue As Double
            Try
                dblValue = CType(strValue, Double)
                isFloatString = True
            Catch ex As Exception
                isFloatString = False
            End Try

        End Function

        '----------------------------------------------------------------
        ' �Ƿ�Ϊ������ֵ
        '----------------------------------------------------------------
        Public Function isDatetimeString(ByVal strValue As String) As Boolean

            Dim dateValue As DateTime
            Try
                dateValue = CType(strValue, DateTime)
                isDatetimeString = True
            Catch ex As Exception
                isDatetimeString = False
            End Try

        End Function









        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��String �汾(�ü��ո�)
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As String) As String

            Try
                getObjectValue = CType(value, String)
                If getObjectValue.Length > 0 Then
                    getObjectValue = getObjectValue.Trim()
                End If
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��String �汾(���ü��ո�)
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As String, ByVal blnNoTrim As Boolean) As String

            Try
                getObjectValue = CType(value, String)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��DateTime �汾
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As System.DateTime) As System.DateTime

            Try
                getObjectValue = CType(value, System.DateTime)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��DateTime �汾
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Dim objDateTime As System.DateTime
            Try
                objDateTime = CType(value, System.DateTime)
                getObjectValue = objDateTime.ToString(strFormat)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��Integer �汾
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Integer) As Integer

            Try
                getObjectValue = CType(value, Integer)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��Long �汾
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Long) As Long

            Try
                getObjectValue = CType(value, Long)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��double �汾
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Double) As Double

            Try
                getObjectValue = CType(value, Double)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��Boolean �汾
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Boolean) As Boolean

            Try
                getObjectValue = CType(value, Boolean)
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�����ֵ��Byte() �汾
        '----------------------------------------------------------------
        Public Function getObjectValue( _
            ByVal value As Object, _
            ByVal defvalue As Byte()) As Byte()

            Try
                getObjectValue = CType(value, Byte())
            Catch ex As Exception
                getObjectValue = defvalue
            End Try

        End Function










        '----------------------------------------------------------------
        ' ��ȫ��ȡ�������ʾֵ��DateTime �汾
        '----------------------------------------------------------------
        Public Function getDisplayValue( _
            ByVal value As Object, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Dim objDateTime As System.DateTime
            Try
                objDateTime = CType(value, System.DateTime)
                If objDateTime.Year <= 1900 Then
                    getDisplayValue = defvalue
                Else
                    getDisplayValue = objDateTime.ToString(strFormat)
                End If
            Catch ex As Exception
                getDisplayValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�������ʾֵ��Integer �汾
        '----------------------------------------------------------------
        Public Function getDisplayValue( _
            ByVal value As Integer, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Try
                If value = 0 Then
                    getDisplayValue = ""
                Else
                    If strFormat = "" Then
                        getDisplayValue = value.ToString
                    Else
                        getDisplayValue = value.ToString(strFormat)
                    End If
                End If
            Catch ex As Exception
                getDisplayValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�������ʾֵ��Long �汾
        '----------------------------------------------------------------
        Public Function getDisplayValue( _
            ByVal value As Long, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Try
                If value = 0 Then
                    getDisplayValue = ""
                Else
                    If strFormat = "" Then
                        getDisplayValue = value.ToString
                    Else
                        getDisplayValue = value.ToString(strFormat)
                    End If
                End If
            Catch ex As Exception
                getDisplayValue = defvalue
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ȫ��ȡ�������ʾֵ��double �汾
        '----------------------------------------------------------------
        Public Function getDisplayValue( _
            ByVal value As Double, _
            ByVal defvalue As String, _
            ByVal strFormat As String) As String

            Try
                If value = 0 Then
                    getDisplayValue = ""
                Else
                    If strFormat = "" Then
                        getDisplayValue = value.ToString
                    Else
                        getDisplayValue = value.ToString(strFormat)
                    End If
                End If
            Catch ex As Exception
                getDisplayValue = defvalue
            End Try

        End Function











        '----------------------------------------------------------------
        ' ���ݸ������Ȼ�ȡ��Ӧ�ּ�����ļ��𣬴�1����ʼ
        '     intFJCDSM     ���ּ���������ܳ���
        '     intCodeLen    ��Ҫ���Ĵ��볤��
        ' ����
        '                   ��ָ������(-1��ʾ����)
        '----------------------------------------------------------------
        Public Function getCodeLevel( _
            ByVal intFJCDSM() As Integer, _
            ByVal intCodeLen As Integer) As Integer

            Try
                If intCodeLen = 0 Then
                    getCodeLevel = -1
                Else
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = intFJCDSM.Length
                    For i = 0 To intCount - 1 Step 1
                        If intFJCDSM(i) = intCodeLen Then
                            getCodeLevel = i + 1
                            Exit Try
                        End If
                    Next
                End If
            Catch ex As Exception
                getCodeLevel = -1
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݸ������Ȼ�ȡ��Ӧ�ּ�����ļ��𣬴�1����ʼ
        '     strCodeValue  ������ֵ
        '     strCodeSep    ������ָ���
        ' ����
        '                   ��ָ������(-1��ʾ����)
        '----------------------------------------------------------------
        Public Function getCodeLevel( _
            ByVal strCodeValue As String, _
            ByVal strCodeSep As String) As Integer

            Dim intLevel As Integer = -1

            Try
                If strCodeValue Is Nothing Then strCodeValue = ""
                strCodeValue = strCodeValue.Trim()

                If strCodeValue <> "" Then
                    Dim strIndex() As String
                    strIndex = strCodeValue.Split(strCodeSep.ToCharArray())
                    intLevel = strIndex.Length
                End If
            Catch ex As Exception
            End Try

            getCodeLevel = intLevel

        End Function

        '----------------------------------------------------------------
        ' ������볤�ȵĺϷ���
        '     intFJCDSM     ���ּ���������ܳ���
        '     intCodeLen    ��Ҫ���Ĵ��볤��
        ' ����
        '     True          ���Ϸ�
        '     False         �����Ϸ�
        '----------------------------------------------------------------
        Public Function doVerifyCodeLength( _
            ByVal intFJCDSM() As Integer, _
            ByVal intCodeLen As Integer) As Boolean

            doVerifyCodeLength = False
            Try
                If intCodeLen = 0 Then
                    doVerifyCodeLength = True
                Else
                    Dim intCount As Integer
                    Dim i As Integer
                    intCount = intFJCDSM.Length
                    For i = 0 To intCount - 1 Step 1
                        If intFJCDSM(i) = intCodeLen Then
                            doVerifyCodeLength = True
                            Exit Try
                        End If
                    Next
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ݸ��������ȡ�ϼ�����
        '     intFJCDSM     ���ּ���������ܳ���
        '     strCode       ����ǰ����
        ' ����
        '                   ���ϼ�����ֵ
        '----------------------------------------------------------------
        Public Function getPrevLevelCode( _
            ByVal intFJCDSM() As Integer, _
            ByVal strCode As String) As String

            Dim strValue As String = ""

            Try
                If strCode Is Nothing Then strCode = ""
                strCode = strCode.Trim()

                If strCode <> "" Then
                    Dim intCount As Integer
                    Dim intLen As Integer
                    Dim i As Integer
                    intLen = strCode.Length
                    intCount = intFJCDSM.Length
                    For i = 0 To intCount - 1 Step 1
                        If intLen = intFJCDSM(i) Then
                            If i > 0 Then
                                strValue = strCode.Substring(0, intFJCDSM(i - 1))
                            End If
                            Exit Try
                        End If
                    Next
                End If
            Catch ex As Exception
            End Try

            getPrevLevelCode = strValue

        End Function

        '----------------------------------------------------------------
        ' ���ݸ��������ȡ�ϼ�����
        '     strCodeValue  ������ֵ
        '     strCodeSep    ������ָ���
        ' ����
        '                   ���ϼ�����ֵ
        '----------------------------------------------------------------
        Public Function getPrevLevelCode( _
            ByVal strCodeValue As String, _
            ByVal strCodeSep As String) As String

            Dim strValue As String = ""

            Try
                If strCodeValue Is Nothing Then strCodeValue = ""
                If strCodeSep Is Nothing Then strCodeSep = ""
                strCodeValue = strCodeValue.Trim()
                strCodeSep = strCodeSep.Trim()

                If strCodeValue <> "" Then
                    Dim strIndex() As String
                    Dim intCount As Integer
                    Dim i As Integer
                    strIndex = strCodeValue.Split(strCodeSep.ToCharArray())
                    intCount = strIndex.Length
                    For i = 0 To intCount - 2 Step 1
                        If strValue = "" Then
                            strValue = strIndex(i)
                        Else
                            strValue = strValue + strCodeSep + strIndex(i)
                        End If
                    Next
                End If
            Catch ex As Exception
            End Try

            getPrevLevelCode = strValue

        End Function

        '----------------------------------------------------------------
        ' ���ݸ���strCodeValue��ȡָ������Ĵ���ֵ
        '     strCodeValue  ������ֵ
        '     strCodeSep    ������ָ���
        '     intLevel      �����뼶��
        ' ����
        '                   ��ָ������ı�������
        '----------------------------------------------------------------
        Public Function getCodeValue( _
            ByVal strCodeValue As String, _
            ByVal strCodeSep As String, _
            ByVal intLevel As Integer) As String

            Dim strValue As String = ""

            Try
                If strCodeValue Is Nothing Then strCodeValue = ""
                If strCodeSep Is Nothing Then strCodeSep = ""
                strCodeValue = strCodeValue.Trim()
                strCodeSep = strCodeSep.Trim()
                If intLevel < 1 Then Exit Try
                If strCodeValue = "" Then Exit Try

                Dim strIndex() As String
                Dim intCount As Integer
                Dim i As Integer
                strIndex = strCodeValue.Split(strCodeSep.ToCharArray())
                If intLevel - 1 > strIndex.Length Then Exit Try
                strValue = strIndex(intLevel - 1)
            Catch ex As Exception
            End Try

            getCodeValue = strValue

        End Function

        '----------------------------------------------------------------
        ' ���ݸ���strCodeValue��ȡָ������Ĵ���ֵ
        '     strCodeValue  ������ֵ
        '     strCodeSep    ������ָ���
        '     intLevel      �����뼶��
        '     blnUnused     ��������
        ' ����
        '                   ��ָ���������ȫ����
        '----------------------------------------------------------------
        Public Function getCodeValue( _
            ByVal strCodeValue As String, _
            ByVal strCodeSep As String, _
            ByVal intLevel As Integer, _
            ByVal blnUnused As Boolean) As String

            Dim strValue As String = ""

            Try
                If strCodeValue Is Nothing Then strCodeValue = ""
                If strCodeSep Is Nothing Then strCodeSep = ""
                strCodeValue = strCodeValue.Trim()
                strCodeSep = strCodeSep.Trim()
                If intLevel < 1 Then Exit Try
                If strCodeValue = "" Then Exit Try

                Dim strIndex() As String
                Dim intCount As Integer
                Dim i As Integer
                strIndex = strCodeValue.Split(strCodeSep.ToCharArray())
                If intLevel > strIndex.Length Then Exit Try
                For i = 0 To intLevel - 1 Step 1
                    If strValue = "" Then
                        strValue = strIndex(i)
                    Else
                        strValue = strValue + strCodeSep + strIndex(i)
                    End If
                Next
            Catch ex As Exception
            End Try

            getCodeValue = strValue

        End Function










        '----------------------------------------------------------------
        ' ��ָ���ַ���ǰ����������ַ�
        '     strSrc        ��Դ�ַ���
        '     intLen        ��������ַ�������
        '     strFill       �����������ַ�
        '     blnFront      ��True-��ǰ����䣬False-�ں������
        ' ����
        '                   ��������ַ���
        '----------------------------------------------------------------
        Public Function doFillString( _
            ByVal strSrc As String, _
            ByVal intLen As Integer, _
            ByVal strFill As String, _
            ByVal blnFront As Boolean) As String

            Dim strValue As String = ""
            Dim intCount As Integer
            Dim i As Integer

            Try
                If strSrc Is Nothing Then strSrc = ""
                If strFill Is Nothing Then strFill = ""
                strSrc = strSrc.Trim()
                strFill = strFill.Trim()

                intCount = intLen - strSrc.Length
                strValue = strSrc
                For i = 0 To intCount - 1 Step 1
                    If blnFront = True Then
                        strValue = strFill + strValue
                    Else
                        strValue = strValue + strFill
                    End If
                Next

                doFillString = strValue

            Catch ex As Exception
                doFillString = strSrc
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��ANSI�ַ���ת��ΪHTML��ʾ���ַ���
        '     strSrc        ��Դ�ַ���
        ' ����
        '                   ��ת������ַ���
        '----------------------------------------------------------------
        Public Function doConvertToHtml(ByVal strSrc As String) As String

            Dim strTemp As String = ""
            Try
                If strSrc Is Nothing Then strSrc = ""
                strTemp = strSrc

                strTemp = strTemp.Replace(Chr(13), "<br>")
                strTemp = strTemp.Replace(" ", "&nbsp;&nbsp;")
                strTemp = strTemp.Replace(Chr(10), "")

                doConvertToHtml = strTemp

            Catch ex As Exception
                doConvertToHtml = strTemp
            End Try

        End Function

        '----------------------------------------------------------------
        ' ���ַ����еķָ���ת��Ϊ��׼�ָ���
        '     strSrc        ��Դ�ַ���
        ' ����
        '                   ��ת������ַ���
        '----------------------------------------------------------------
        Public Function doTranslateSeperate(ByVal strSrc As String) As String

            Dim strTemp As String = ""
            Try
                Dim strSep As String = Me.CharSeparate
                If strSrc Is Nothing Then strSrc = ""
                strTemp = strSrc

                strTemp = strTemp.Replace("��", strSep)
                strTemp = strTemp.Replace("��", strSep)
                strTemp = strTemp.Replace("��", strSep)
                strTemp = strTemp.Replace("��", strSep)

                strTemp = strTemp.Replace(";", strSep)
                strTemp = strTemp.Replace(":", strSep)

                doTranslateSeperate = strTemp

            Catch ex As Exception
                doTranslateSeperate = strTemp
            End Try

        End Function

        '----------------------------------------------------------------
        ' ������ת��Ϊ�ַ�����ʾ(1901-1-1 00:00:00��ǰ�����ڼ�Ϊ������)
        '     objDate       ������ֵ
        '     strFormat     ����ʽ���ַ���
        ' ����
        '                   ��ת������ַ���
        '----------------------------------------------------------------
        Public Function doDateToString( _
            ByVal objDate As System.DateTime, _
            ByVal strFormat As String) As String

            Dim strTemp As String = ""
            Try
                If strFormat Is Nothing Then strFormat = ""
                strFormat = strFormat.Trim()
                If strFormat = "" Then Exit Try

                Dim objNullDate As New System.DateTime(1901, 1, 1, 0, 0, 0)
                If objDate < objNullDate Then
                Else
                    strTemp = Format(objDate, strFormat)
                End If
                doDateToString = strTemp

            Catch ex As Exception
                doDateToString = strTemp
            End Try

        End Function








        '----------------------------------------------------------------
        ' ��Rijndael�㷨�����ļ�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFileSpec          ��Ҫ���ܵ��ļ�������·��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' ��ע
        '      ������ɺ�����ܺ���ļ�д���ض�����ͷ��Ϣ
        '     �Ѿ����ܵ��ļ�����ִ�м���
        '----------------------------------------------------------------
        Public Function doEncryptFile( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

            Dim objICryptoTransform As System.Security.Cryptography.ICryptoTransform = Nothing
            Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged = Nothing
            Dim objCryptoStream As System.Security.Cryptography.CryptoStream = Nothing
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objSrcFileStream As System.IO.FileStream = Nothing
            Dim objSrcFileInfo As System.IO.FileInfo = Nothing
            Dim objDesFileStream As System.IO.FileStream = Nothing
            Dim objDesFileInfo As System.IO.FileInfo = Nothing
            Dim intBufSize As Integer = 1024 'Buffer Size(byte)
            Dim blnDelete As Boolean = False
            Dim strBakFile As String = ""

            doEncryptFile = False
            strErrMsg = ""

            Try
                '�ж��Ƿ����(32bytes)
                objSrcFileInfo = New System.IO.FileInfo(strFileSpec)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                Dim byteKey(Me.m_bKey.Length - 1) As Byte
                Dim byteIV(Me.m_bIV.Length - 1) As Byte
                objSrcFileStream.Read(byteKey, 0, Me.m_bKey.Length)
                objSrcFileStream.Read(byteIV, 0, Me.m_bIV.Length)
                Dim blnFound As Boolean = False
                Dim i As Integer = 0
                For i = 0 To byteKey.Length - 1 Step 1
                    If byteKey(i) <> Me.m_bKey(i) Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    For i = 0 To byteIV.Length - 1 Step 1
                        If byteIV(i) <> Me.m_bIV(i) Then
                            blnFound = True
                            Exit For
                        End If
                    Next
                End If
                If blnFound = False Then
                    '�Ѿ����ܣ����ü���
                    Exit Try
                End If
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                System.Threading.Thread.Sleep(15)

                '�����ļ���strFileSpec + .org
                strBakFile = strFileSpec + ".org"
                If objBaseLocalFile.doCopyFile(strErrMsg, strFileSpec, strBakFile, True) = False Then
                    GoTo errProc
                End If
                blnDelete = True
                System.Threading.Thread.Sleep(15)

                '��ȡ������
                objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged
                objICryptoTransform = objRijndaelManaged.CreateEncryptor(Me.m_bKey, Me.m_bIV)

                '��������������
                objDesFileInfo = New System.IO.FileInfo(strBakFile)
                objDesFileStream = objDesFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Write, System.IO.FileShare.None)
                objCryptoStream = New System.Security.Cryptography.CryptoStream( _
                    objDesFileStream, _
                    objICryptoTransform, _
                    System.Security.Cryptography.CryptoStreamMode.Write)

                '�����ļ�
                Dim byteSrc(intBufSize - 1) As Byte
                Dim intRead As Integer = 0
                objSrcFileInfo = New System.IO.FileInfo(strFileSpec)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                While True
                    intRead = objSrcFileStream.Read(byteSrc, 0, byteSrc.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    objCryptoStream.Write(byteSrc, 0, intRead)
                End While
                objCryptoStream.FlushFinalBlock()

                '�ر��ļ�
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                System.Threading.Thread.Sleep(15)

                'д�����ļ�ͷ(32bytes)
                objDesFileInfo = New System.IO.FileInfo(strFileSpec)
                objDesFileStream = objDesFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Write, System.IO.FileShare.None)
                objDesFileStream.Write(Me.m_bKey, 0, Me.m_bKey.Length)
                objDesFileStream.Write(Me.m_bIV, 0, Me.m_bIV.Length)
                objSrcFileInfo = New System.IO.FileInfo(strBakFile)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                While True
                    intRead = objSrcFileStream.Read(byteSrc, 0, byteSrc.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    objDesFileStream.Write(byteSrc, 0, intRead)
                End While
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                System.Threading.Thread.Sleep(15)

                'ɾ����ʱ�ļ�
                If blnDelete = True And strBakFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strBakFile) = False Then
                        '����
                    End If
                    System.Threading.Thread.Sleep(15)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)

            doEncryptFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)
            If blnDelete = True And strBakFile <> "" Then
                Dim strErrMsgA As String = ""
                objBaseLocalFile.doDeleteFile(strErrMsgA, strBakFile)
            End If
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��Rijndael�㷨�����ļ�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strFileSpec          ��Ҫ���ܵ��ļ�������·��
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        ' ��ע
        '      �����ļ�ǰ����Ƿ����ض������ļ�ͷ
        '----------------------------------------------------------------
        Public Function doDecryptFile( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

            Dim objICryptoTransform As System.Security.Cryptography.ICryptoTransform = Nothing
            Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged = Nothing
            Dim objCryptoStream As System.Security.Cryptography.CryptoStream = Nothing
            Dim objBaseLocalFile As New Xydc.Platform.Common.Utilities.BaseLocalFile
            Dim objSrcFileStream As System.IO.FileStream = Nothing
            Dim objSrcFileInfo As System.IO.FileInfo = Nothing
            Dim objDesFileStream As System.IO.FileStream = Nothing
            Dim objDesFileInfo As System.IO.FileInfo = Nothing
            Dim intBufferSize As Integer = 1024 'buffer size (byte)
            Dim blnDelete As Boolean = False
            Dim strBakFile As String = ""

            doDecryptFile = False
            strErrMsg = ""

            Try
                Dim byteBuffer(intBufferSize - 1) As Byte
                Dim intRead As Integer = 0

                '�ж��Ƿ����(32bytes)
                objSrcFileInfo = New System.IO.FileInfo(strFileSpec)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                Dim byteKey(Me.m_bKey.Length - 1) As Byte
                Dim byteIV(Me.m_bIV.Length - 1) As Byte
                objSrcFileStream.Read(byteKey, 0, Me.m_bKey.Length)
                objSrcFileStream.Read(byteIV, 0, Me.m_bIV.Length)
                Dim blnFound As Boolean = False
                Dim i As Integer = 0
                For i = 0 To byteKey.Length - 1 Step 1
                    If byteKey(i) <> Me.m_bKey(i) Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    For i = 0 To byteIV.Length - 1 Step 1
                        If byteIV(i) <> Me.m_bIV(i) Then
                            blnFound = True
                            Exit For
                        End If
                    Next
                End If
                If blnFound = True Then
                    'û�м��ܣ����ý���
                    Exit Try
                End If

                'д��ʵ�ļ����ݵ�strBakFile
                strBakFile = strFileSpec + ".org"
                objDesFileInfo = New System.IO.FileInfo(strBakFile)
                objDesFileStream = objDesFileInfo.Open(System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None)
                While True
                    intRead = objSrcFileStream.Read(byteBuffer, 0, byteBuffer.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    objDesFileStream.Write(byteBuffer, 0, intRead)
                End While
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
                blnDelete = True
                System.Threading.Thread.Sleep(15)

                '��ȡ������
                objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged
                objICryptoTransform = objRijndaelManaged.CreateDecryptor(Me.m_bKey, Me.m_bIV)

                '��������������
                objSrcFileInfo = New System.IO.FileInfo(strBakFile)
                objSrcFileStream = objSrcFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None)
                objCryptoStream = New System.Security.Cryptography.CryptoStream( _
                 objSrcFileStream, _
                 objICryptoTransform, _
                 System.Security.Cryptography.CryptoStreamMode.Read)

                '�����ļ�
                objDesFileInfo = New System.IO.FileInfo(strFileSpec)
                objDesFileStream = objDesFileInfo.Open(System.IO.FileMode.Open, System.IO.FileAccess.Write, System.IO.FileShare.None)
                While True
                    intRead = objCryptoStream.Read(byteBuffer, 0, byteBuffer.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    objDesFileStream.Write(byteBuffer, 0, intRead)
                End While

                '�ر��ļ�
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
                Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
                System.Threading.Thread.Sleep(15)

                'ɾ����ʱ�ļ�
                If blnDelete = True And strBakFile <> "" Then
                    If objBaseLocalFile.doDeleteFile(strErrMsg, strBakFile) = False Then
                        '����
                    End If
                    System.Threading.Thread.Sleep(15)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)

            doDecryptFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objDesFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objSrcFileInfo)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)
            If blnDelete = True And strBakFile <> "" Then
                Dim strErrMsgA As String = ""
                objBaseLocalFile.doDeleteFile(strErrMsgA, strBakFile)
            End If
            Xydc.Platform.Common.Utilities.BaseLocalFile.SafeRelease(objBaseLocalFile)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��Rijndael�㷨�����ַ�������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     strData              ��Ҫ���ܵ��ַ���
        '     bData                �����ܺ���ֽ�����(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doEncryptString( _
            ByRef strErrMsg As String, _
            ByVal strData As String, _
            ByRef bData() As Byte) As Boolean

            Dim objICryptoTransform As System.Security.Cryptography.ICryptoTransform = Nothing
            Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged = Nothing
            Dim objCryptoStream As System.Security.Cryptography.CryptoStream = Nothing
            Dim objMemoryStream As System.IO.MemoryStream = Nothing

            doEncryptString = False

            Try
                '��ȡ������
                objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged
                objICryptoTransform = objRijndaelManaged.CreateEncryptor(Me.m_bKey, Me.m_bIV)

                '��������������
                objMemoryStream = New System.IO.MemoryStream
                objCryptoStream = New System.Security.Cryptography.CryptoStream( _
                    objMemoryStream, _
                    objICryptoTransform, _
                    System.Security.Cryptography.CryptoStreamMode.Write)

                '���ַ���ת��Ϊ�ֽ�����
                Dim bSrc() As Byte = System.Text.Encoding.Unicode.GetBytes(strData)

                '���ֽ�����д�����������
                objCryptoStream.Write(bSrc, 0, bSrc.Length)
                objCryptoStream.FlushFinalBlock()

                '��ȡ���ܺ������
                bData = objMemoryStream.ToArray()
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objMemoryStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)

            doEncryptString = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objMemoryStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��Rijndael�㷨���ܼ��ܺ���ֽ�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     bData                �����ܺ���ֽ�����
        '     strData              ��Ҫ���ܵ��ַ���(����)
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doDecryptString( _
            ByRef strErrMsg As String, _
            ByVal bData() As Byte, _
            ByRef strData As String) As Boolean

            Dim objICryptoTransform As System.Security.Cryptography.ICryptoTransform = Nothing
            Dim objRijndaelManaged As System.Security.Cryptography.RijndaelManaged = Nothing
            Dim objCryptoStream As System.Security.Cryptography.CryptoStream = Nothing
            Dim objMemoryStream As System.IO.MemoryStream = Nothing
            Dim objDesStream As System.IO.MemoryStream = Nothing

            doDecryptString = False
            strData = ""

            Try
                '���
                If bData Is Nothing Then Exit Try
                If bData.Length < 1 Then Exit Try

                '��ȡ������
                objRijndaelManaged = New System.Security.Cryptography.RijndaelManaged
                objICryptoTransform = objRijndaelManaged.CreateDecryptor(Me.m_bKey, Me.m_bIV)

                '��������������
                objMemoryStream = New System.IO.MemoryStream(bData)
                objCryptoStream = New System.Security.Cryptography.CryptoStream( _
                 objMemoryStream, _
                 objICryptoTransform, _
                 System.Security.Cryptography.CryptoStreamMode.Read)

                '�ӽ����������ж�ȡ��������
                Dim byteBuffer() As Byte = {}
                Dim intSize As Integer = 0
                Dim intBufferSize As Integer = 1024
                Dim bDes(intBufferSize - 1) As Byte
                Dim intRead As Integer = 0
                Dim i As Integer = 0
                While True
                    intRead = objCryptoStream.Read(bDes, 0, bDes.Length)
                    If intRead = 0 Then
                        Exit While
                    End If
                    ReDim Preserve byteBuffer(intSize + intRead)
                    For i = 0 To intRead - 1 Step 1
                        byteBuffer(intSize + i) = bDes(i)
                    Next
                    intSize += intRead
                End While

                '��ȡ���ܺ������
                strData = System.Text.Encoding.Unicode.GetString(byteBuffer)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objMemoryStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)

            doDecryptString = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objCryptoStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objMemoryStream)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objICryptoTransform)
            Xydc.Platform.Common.Utilities.ResourceManager.SafeRelease(objRijndaelManaged)
            Exit Function

        End Function











        '----------------------------------------------------------------
        ' ����ָ�����ڻ�ȡ�������ڵĿ�ʼ����=�����ա���������=������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDay               ����ǰ����
        '     objWeekStart         ��(����)�����յ�����
        '     objWeekEnd           ��(����)������������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime, _
            ByRef objWeekEnd As System.DateTime) As Boolean

            getWeekStartAndEndDay = False
            strErrMsg = ""

            Try
                Dim intAdd As Integer
                Select Case objDay.DayOfWeek()
                    Case DayOfWeek.Sunday
                        intAdd = 0
                    Case DayOfWeek.Monday
                        intAdd = -1
                    Case DayOfWeek.Tuesday
                        intAdd = -2
                    Case DayOfWeek.Wednesday
                        intAdd = -3
                    Case DayOfWeek.Thursday
                        intAdd = -4
                    Case DayOfWeek.Friday
                        intAdd = -5
                    Case DayOfWeek.Saturday
                        intAdd = -6
                End Select

                '��ȡ�����յ���
                objWeekStart = objDay.AddDays(intAdd)

                '��ȡ����������
                objWeekEnd = objWeekStart.AddDays(6)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ�����ڻ�ȡ�������ڵĿ�ʼ����=�����ա���������=������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDay               ����ǰ����
        '     objWeekStart         ��(����)�����յ�����
        '     objWeekEnd           ��(����)������������
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime, _
            ByRef objWeekEnd As System.DateTime, _
            ByVal SaturdaytoFriday As String) As Boolean

            getWeekStartAndEndDay = False
            strErrMsg = ""

            Try
                Dim intAdd As Integer
                Select Case objDay.DayOfWeek()
                    Case DayOfWeek.Sunday
                        intAdd = -1
                    Case DayOfWeek.Monday
                        intAdd = -2
                    Case DayOfWeek.Tuesday
                        intAdd = -3
                    Case DayOfWeek.Wednesday
                        intAdd = -4
                    Case DayOfWeek.Thursday
                        intAdd = -5
                    Case DayOfWeek.Friday
                        intAdd = -6
                    Case DayOfWeek.Saturday
                        intAdd = 0
                End Select

                '��ȡ����������
                objWeekStart = objDay.AddDays(intAdd)

                '��ȡ���������
                objWeekEnd = objWeekStart.AddDays(6)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ�����ڻ�ȡ���ܵĿ�ʼ����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDay               ����ǰ����
        '     objWeekStart         ��(����)���ܵĿ�ʼ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime) As Boolean

            getWeekStartAndEndDay = False
            strErrMsg = ""

            Try
                '��ȡ���ܵĿ�ʼ����
                objWeekStart = objDay.AddDays(-7).AddDays(1)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ�����ڻ�ȡ��һ�ܵĿ�ʼ���ڣ���������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDay               ����ǰ����
        '     objWeekStart         ��(����)���ܵĿ�ʼ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNextWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime, _
            ByRef objWeekEnd As System.DateTime) As Boolean

            getNextWeekStartAndEndDay = False
            strErrMsg = ""
            Dim objDayTemp As System.DateTime

            Try
                objDayTemp = objDay.AddDays(7)

                Dim intAdd As Integer
                Select Case objDayTemp.DayOfWeek()
                    Case DayOfWeek.Sunday
                        intAdd = 0
                    Case DayOfWeek.Monday
                        intAdd = -1
                    Case DayOfWeek.Tuesday
                        intAdd = -2
                    Case DayOfWeek.Wednesday
                        intAdd = -3
                    Case DayOfWeek.Thursday
                        intAdd = -4
                    Case DayOfWeek.Friday
                        intAdd = -5
                    Case DayOfWeek.Saturday
                        intAdd = -6
                End Select

                '��ȡ�����յ���
                objWeekStart = objDayTemp.AddDays(intAdd)

                '��ȡ����������
                objWeekEnd = objWeekStart.AddDays(6)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNextWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ�����ڻ�ȡ��һ�ܵĿ�ʼ���ڣ���������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDay               ����ǰ����
        '     objWeekStart         ��(����)���ܵĿ�ʼ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getLastWeekStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objWeekStart As System.DateTime, _
            ByRef objWeekEnd As System.DateTime) As Boolean

            getLastWeekStartAndEndDay = False
            strErrMsg = ""
            Dim objDayTemp As System.DateTime

            Try
                objDayTemp = objDay.AddDays(-7)

                Dim intAdd As Integer
                Select Case objDayTemp.DayOfWeek()
                    Case DayOfWeek.Sunday
                        intAdd = 0
                    Case DayOfWeek.Monday
                        intAdd = -1
                    Case DayOfWeek.Tuesday
                        intAdd = -2
                    Case DayOfWeek.Wednesday
                        intAdd = -3
                    Case DayOfWeek.Thursday
                        intAdd = -4
                    Case DayOfWeek.Friday
                        intAdd = -5
                    Case DayOfWeek.Saturday
                        intAdd = -6
                End Select

                '��ȡ�����յ���
                objWeekStart = objDayTemp.AddDays(intAdd)

                '��ȡ����������
                objWeekEnd = objWeekStart.AddDays(6)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getLastWeekStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' �������ֻ�ȡ��Ӧ�������·�����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intDay               ����ǰ��Ӧ�·�����
        '     strMonth             ��(����)�����·�����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getChineseMonth( _
            ByRef strErrMsg As String, _
            ByVal intDay As Integer, _
            ByRef strMonth As String) As Boolean

            getChineseMonth = False
            strMonth = ""
            strErrMsg = ""

            Try
                Select Case intDay
                    Case 1
                        strMonth = "һ��"
                    Case 2
                        strMonth = "����"
                    Case 3
                        strMonth = "����"
                    Case 4
                        strMonth = "����"
                    Case 5
                        strMonth = "����"
                    Case 6
                        strMonth = "����"
                    Case 7
                        strMonth = "����"
                    Case 8
                        strMonth = "����"
                    Case 9
                        strMonth = "����"
                    Case 10
                        strMonth = "ʮ��"
                    Case 11
                        strMonth = "ʮһ��"
                    Case 12
                        strMonth = "ʮ����"
                End Select
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getChineseMonth = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ�����ڻ�ȡ���ܵĿ�ʼ����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDay               ����ǰ����
        '     objWeekStart         ��(����)���ܵĿ�ʼ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getChineseWeek( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef strWeekName As String) As Boolean

            getChineseWeek = False
            strWeekName = ""
            strErrMsg = ""

            Try
                Select Case objDay.DayOfWeek
                    Case DayOfWeek.Sunday
                        strWeekName = "������"
                    Case DayOfWeek.Monday
                        strWeekName = "����һ"
                    Case DayOfWeek.Tuesday
                        strWeekName = "���ڶ�"
                    Case DayOfWeek.Wednesday
                        strWeekName = "������"
                    Case DayOfWeek.Thursday
                        strWeekName = "������"
                    Case DayOfWeek.Friday
                        strWeekName = "������"
                    Case DayOfWeek.Saturday
                        strWeekName = "������"
                End Select
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getChineseWeek = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ�����ڻ�ȡ�����·ݵĿ�ʼ���ڡ���������
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDay               ����ǰ����
        '     objMonthStart        ��(����)�³�����
        '     objMonthEnd          ��(����)��ĩ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMonthStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objMonthStart As System.DateTime, _
            ByRef objMonthEnd As System.DateTime) As Boolean

            getMonthStartAndEndDay = False
            strErrMsg = ""

            Try
                '��ȡ�³�����
                objMonthStart = CType(Format(objDay, "yyyy-MM") + "-01", System.DateTime)

                '��ȡ��ĩ����
                objMonthEnd = objMonthStart.AddMonths(1).AddDays(-1)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getMonthStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ�����ڻ�ȡ��������ǰһ�µĿ�ʼ����
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     objDay               ����ǰ����
        '     objMonthStart        ��(����)��ʼ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getMonthStartAndEndDay( _
            ByRef strErrMsg As String, _
            ByVal objDay As System.DateTime, _
            ByRef objMonthStart As System.DateTime) As Boolean

            getMonthStartAndEndDay = False
            strErrMsg = ""

            Try
                '��ȡ��ʼ����
                objMonthStart = objDay.AddMonths(1).AddDays(1)

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getMonthStartAndEndDay = True
            Exit Function
errProc:
            Exit Function

        End Function










        '****************************************************************
        ' ���д���Ϊ�̶����ȷּ����봦������
        '****************************************************************
        '----------------------------------------------------------------
        ' ����ѡ�������ȡ��Ӧ���´�����ϼ�����ֵ���´���ĳ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intFJCDSM            ������ּ�������������
        '     strFillChar          �����볤�Ȳ���ʱ������ַ�
        '     strXZDM              ����ǰѡ������
        '     blnIsTJ              ��=true�´����뵱ǰѡ������ͬ��,=false�´���Ϊ��ǰѡ�������¼�
        '     strSJDMALL           ���´�����ϼ�����ֵ��ȫ����ֵ
        '     strSJDM              ���´�����ϼ�����ֵ(ֻ��ָ������ĳ��ȣ������Ϊ��)
        '     intDMCD              ���´���ĳ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewDMParamtersFixLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strFillChar As String, _
            ByVal strXZDM As String, _
            ByVal blnIsTJ As Boolean, _
            ByRef strSJDMALL As String, _
            ByRef strSJDM As String, _
            ByRef intDMCD As Integer) As Boolean

            Dim intTotalLen As Integer
            Dim intLen As Integer
            Dim i As Integer

            getNewDMParamtersFixLen = False
            strSJDMALL = ""
            strErrMsg = ""
            strSJDM = ""
            intDMCD = 0

            Try
                '���
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If strFillChar Is Nothing Then strFillChar = ""
                strFillChar = strFillChar.Trim
                If strFillChar = "" Then
                    strErrMsg = "����δָ������ַ���"
                    GoTo errProc
                End If
                If intFJCDSM Is Nothing Then
                    strErrMsg = "����δָ������ķּ�������Ϣ��"
                    GoTo errProc
                End If

                '��ȡ����ּ���Ϣ
                Dim intCount As Integer = intFJCDSM.Length
                If intCount < 1 Then
                    strErrMsg = "����δָ������ּ������"
                    GoTo errProc
                End If
                intTotalLen = intFJCDSM(intCount - 1)
                If intCount = 1 Then
                    'ֻ��һ������
                    strSJDM = ""
                    intDMCD = intFJCDSM(0)
                    Exit Try
                End If

                '��ȡ���볤��
                Dim intXZDMCD As Integer = Me.getStringLength(strXZDM)
                If intXZDMCD <= 0 Then
                    'ֻ���Ƕ������룡
                    strSJDM = ""
                    intDMCD = intFJCDSM(0)
                    Exit Try
                End If

                '����Ϊ����������У��
                If intXZDMCD <> intTotalLen Then
                    strErrMsg = "����[" + strXZDM + "]����Ч�Ĵ��룡"
                    GoTo errProc
                End If

                'ȷ��ѡ�����뼶��
                Dim strFillStr As String
                Dim strTCM As String
                Dim strJBM As String
                Dim j As Integer
                For i = 0 To intCount - 1 Step 1
                    '�����һ��
                    If i = intCount - 1 Then
                        If blnIsTJ = False Then
                            strErrMsg = "�����Ѿ������һ�����������¼���"
                            GoTo errProc
                        Else
                            strSJDM = strXZDM.Substring(0, intFJCDSM(i - 1))
                            intDMCD = intFJCDSM(i)
                            Exit Try
                        End If
                    End If

                    '������
                    strJBM = strXZDM.Substring(0, intFJCDSM(i))
                    '�����
                    strTCM = strXZDM.Substring(intFJCDSM(i))
                    '�����?
                    intLen = intTotalLen - intFJCDSM(i)
                    strFillStr = ""
                    For j = 0 To intLen - 1 Step 1
                        strFillStr += strFillChar
                    Next
                    If strFillStr <> "" Then
                        If strTCM = strFillStr Then
                            If blnIsTJ = False Then
                                strSJDM = strJBM
                                intDMCD = intFJCDSM(i + 1)
                                Exit Try
                            Else
                                If i = 0 Then
                                    strSJDM = ""
                                    intDMCD = intFJCDSM(0)
                                    Exit Try
                                Else
                                    strSJDM = strXZDM.Substring(0, intFJCDSM(i - 1))
                                    intDMCD = intFJCDSM(i)
                                    Exit Try
                                End If
                            End If
                        End If
                    End If
                Next

                '����
                strErrMsg = "�����޷�ȷ���ϼ����룡"
                GoTo errProc

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '����ϼ�����
            Try
                If strSJDM <> "" Then
                    intLen = Me.getStringLength(strSJDM)
                    intLen = intTotalLen - intLen
                    strSJDMALL = strSJDM
                    For i = 0 To intLen - 1 Step 1
                        strSJDMALL += strFillChar
                    Next
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            getNewDMParamtersFixLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' �����볤���Ƿ���ȷ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intFJCDSM            ������ּ�������������
        '     strXZDM              ����ǰѡ������
        '     blnIs                �������Ƿ���ȷ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doValidLengthFixLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strXZDM As String, _
            ByRef blnIs As Boolean) As Boolean

            doValidLengthFixLen = False
            strErrMsg = "'"
            blnIs = False

            Try
                '���
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If intFJCDSM Is Nothing Then
                    strErrMsg = "����δָ������ķּ�������Ϣ��"
                    GoTo errProc
                End If

                '��ȡ���볤��
                Dim intXZDMCD As Integer = Me.getStringLength(strXZDM)
                Dim intCount As Integer = intFJCDSM.Length
                Dim intTotalLen As Integer = intFJCDSM(intCount - 1)

                '����Ϊ����������У��
                If intXZDMCD <> intTotalLen Then
                    blnIs = False
                Else
                    blnIs = True
                End If

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doValidLengthFixLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' �ж�ѡ�������Ƿ�Ϊ��ϸ�롣���������ϸ���򷵻ز��������Ĵ���ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intFJCDSM            ������ּ�������������
        '     strFillChar          �����볤�Ȳ���ʱ������ַ�
        '     strXZDM              ����ǰѡ������
        '     blnIs                ���Ƿ���ϸ����
        '     strDMJZ              �����ز��������Ĵ���ֵ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function isDetailFixLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strFillChar As String, _
            ByVal strXZDM As String, _
            ByRef blnIs As Boolean, _
            ByRef strDMJZ As String) As Boolean

            Dim intTotalLen As Integer
            Dim intLen As Integer
            Dim i As Integer

            isDetailFixLen = False
            strErrMsg = ""
            blnIs = True
            strDMJZ = ""

            Try
                '���
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If strXZDM = "" Then
                    Exit Try
                End If
                If strFillChar Is Nothing Then strFillChar = ""
                strFillChar = strFillChar.Trim
                If strFillChar = "" Then
                    strErrMsg = "����δָ������ַ���"
                    GoTo errProc
                End If
                If intFJCDSM Is Nothing Then
                    strErrMsg = "����δָ������ķּ�������Ϣ��"
                    GoTo errProc
                End If

                '��ȡ����ּ���Ϣ
                Dim intCount As Integer = intFJCDSM.Length
                If intCount < 1 Then
                    strErrMsg = "����δָ������ּ������"
                    GoTo errProc
                End If
                intTotalLen = intFJCDSM(intCount - 1)

                '�жϴ����Ƿ�Ϊ��ϸ����
                Dim strValue As String
                Dim strXDM As String
                Dim j As Integer
                For i = 0 To intCount - 2 Step 1
                    strXDM = strXZDM.Substring(0, intFJCDSM(i))
                    strValue = strXDM
                    intLen = intTotalLen - intFJCDSM(i)
                    For j = 0 To intLen - 1 Step 1
                        strXDM = strXDM + strFillChar
                    Next
                    If strXDM = strXZDM Then
                        '����ϸ����
                        strDMJZ = strValue
                        blnIs = False
                        Exit Try
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isDetailFixLen = True
            Exit Function
errProc:
            Exit Function
        End Function




        '****************************************************************
        ' ���д���Ϊ�䳤���ȷּ����봦������
        '****************************************************************
        '----------------------------------------------------------------
        ' ����ѡ�������ȡ��Ӧ���´�����ϼ�����ֵ���´���ĳ���
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intFJCDSM            ������ּ�������������
        '     strXZDM              ����ǰѡ������
        '     blnIsTJ              ��=true�´����뵱ǰѡ������ͬ��,=false�´���Ϊ��ǰѡ�������¼�
        '     strSJDMALL           ���´�����ϼ�����ֵ��ȫ����ֵ
        '     strSJDM              ���´�����ϼ�����ֵ(ֻ��ָ������ĳ��ȣ������Ϊ��)
        '     intDMCD              ���´���ĳ���
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function getNewDMParamtersVarLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strXZDM As String, _
            ByVal blnIsTJ As Boolean, _
            ByRef strSJDMALL As String, _
            ByRef strSJDM As String, _
            ByRef intDMCD As Integer) As Boolean

            Dim intLen As Integer
            Dim i As Integer

            getNewDMParamtersVarLen = False
            strSJDMALL = ""
            strErrMsg = ""
            strSJDM = ""
            intDMCD = 0

            Try
                '���
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If intFJCDSM Is Nothing Then
                    strErrMsg = "����δָ������ķּ�������Ϣ��"
                    GoTo errProc
                End If

                '��ȡ����ּ���Ϣ
                Dim intCount As Integer = intFJCDSM.Length
                If intCount < 1 Then
                    strErrMsg = "����δָ������ּ������"
                    GoTo errProc
                End If
                If intCount = 1 Then
                    'ֻ��һ������
                    strSJDM = ""
                    intDMCD = intFJCDSM(0)
                    Exit Try
                End If

                '��ȡ���볤��
                Dim intXZDMCD As Integer = Me.getStringLength(strXZDM)
                If intXZDMCD <= 0 Then
                    'ֻ���Ƕ������룡
                    strSJDM = ""
                    intDMCD = intFJCDSM(0)
                    Exit Try
                End If

                'У����볤��
                Dim blnFound As Boolean = False
                For i = 0 To intCount - 1 Step 1
                    If intXZDMCD = intFJCDSM(i) Then
                        blnFound = True
                        Exit For
                    End If
                Next
                If blnFound = False Then
                    strErrMsg = "���󣺴��볤�Ȳ���ȷ��"
                    GoTo errProc
                End If

                'ȷ��ѡ�����뼶��
                For i = 0 To intCount - 1 Step 1
                    If intXZDMCD = intFJCDSM(i) Then
                        'ѡ������Ϊi��
                        If blnIsTJ = True Then
                            intDMCD = intFJCDSM(i)
                            If i = 0 Then
                                strSJDM = ""
                            Else
                                strSJDM = strXZDM.Substring(0, intFJCDSM(i - 1))
                            End If
                        Else
                            If i = intCount - 1 Then
                                strErrMsg = "�����Ѿ������һ�����룬�������¼���"
                                GoTo errProc
                            Else
                                intDMCD = intFJCDSM(i + 1)
                                strSJDM = strXZDM
                            End If
                        End If
                        Exit Try
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            '�������
            strSJDMALL = strSJDM

            getNewDMParamtersVarLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' �����볤���Ƿ���ȷ��
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intFJCDSM            ������ּ�������������
        '     strXZDM              ����ǰѡ������
        '     blnIs                �������Ƿ���ȷ
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function doValidLengthVarLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strXZDM As String, _
            ByRef blnIs As Boolean) As Boolean

            doValidLengthVarLen = False
            strErrMsg = "'"
            blnIs = False

            Try
                '���
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If intFJCDSM Is Nothing Then
                    strErrMsg = "����δָ������ķּ�������Ϣ��"
                    GoTo errProc
                End If

                '��ȡ���볤��
                Dim intXZDMCD As Integer = Me.getStringLength(strXZDM)
                Dim intCount As Integer = intFJCDSM.Length
                Dim i As Integer

                'У����볤��
                Dim blnFound As Boolean = False
                For i = 0 To intCount - 1 Step 1
                    If intXZDMCD = intFJCDSM(i) Then
                        blnFound = True
                        Exit For
                    End If
                Next

                '����
                blnIs = blnFound

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doValidLengthVarLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' �ж�ѡ�������Ƿ�Ϊ��ϸ�롣���������ϸ���򷵻ز��������Ĵ���ֵ
        '     strErrMsg            ����������򷵻ش�����Ϣ
        '     intFJCDSM            ������ּ�������������
        '     strXZDM              ����ǰѡ������
        '     blnIs                ���Ƿ���ϸ����
        ' ����
        '     True                 ���ɹ�
        '     False                ��ʧ��
        '----------------------------------------------------------------
        Public Function isDetailVarLen( _
            ByRef strErrMsg As String, _
            ByVal intFJCDSM() As Integer, _
            ByVal strXZDM As String, _
            ByRef blnIs As Boolean) As Boolean

            Dim intLen As Integer
            Dim i As Integer

            isDetailVarLen = False
            strErrMsg = ""
            blnIs = True

            Try
                '���
                If strXZDM Is Nothing Then strXZDM = ""
                strXZDM = strXZDM.Trim
                If strXZDM = "" Then
                    Exit Try
                End If
                If intFJCDSM Is Nothing Then
                    strErrMsg = "����δָ������ķּ�������Ϣ��"
                    GoTo errProc
                End If

                '��ȡ����ּ���Ϣ
                Dim intCount As Integer = intFJCDSM.Length
                If intCount < 1 Then
                    strErrMsg = "����δָ������ּ������"
                    GoTo errProc
                End If

                '�жϴ����Ƿ�Ϊ��ϸ����
                intLen = Me.getStringLength(strXZDM)
                For i = 0 To intCount - 2 Step 1
                    If intLen = intFJCDSM(i) Then
                        blnIs = False
                        Exit Try
                    End If
                Next

            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            isDetailVarLen = True
            Exit Function
errProc:
            Exit Function
        End Function

        '��ȡ����strDigital��Ӧ�ĺ��ֱ�ʾ(��һ������...)
        Public Function getChineseNumber(ByVal strDigital As String) As String

            Dim strTemp(10) As String
            Dim intCount As Integer
            Dim i As Integer

            '��ʼ��
            strDigital = strDigital.Trim
            getChineseNumber = strDigital

            Try
                strTemp(0) = strDigital

                'ת��
                intCount = Len(strTemp(0))
                strTemp(1) = ""
                For i = 1 To intCount Step 1
                    strTemp(2) = Mid(strTemp(0), i, 1)
                    Select Case strTemp(2)
                        Case "0"
                            strTemp(3) = "��"
                        Case "1"
                            strTemp(3) = "һ"
                        Case "2"
                            strTemp(3) = "��"
                        Case "3"
                            strTemp(3) = "��"
                        Case "4"
                            strTemp(3) = "��"
                        Case "5"
                            strTemp(3) = "��"
                        Case "6"
                            strTemp(3) = "��"
                        Case "7"
                            strTemp(3) = "��"
                        Case "8"
                            strTemp(3) = "��"
                        Case "9"
                            strTemp(3) = "��"
                    End Select
                    strTemp(1) = strTemp(1) + strTemp(3)
                Next

                getChineseNumber = strTemp(1)

            Catch ex As Exception
            End Try

        End Function

        '��ȡ����strDigital��Ӧ�ĺ��ֱ�ʾ(1.)
        Public Function getChineseNumber(ByVal strErrMsg As String, ByVal strDigital As String) As Integer

            Dim strTemp As String
            Dim intCount As Integer
            Dim i As Integer

            '��ʼ��
            strDigital = strDigital.Trim
            getChineseNumber = 0

            Try
                Select Case strDigital
                    Case "һ��"
                        intCount = 1
                    Case "����"
                        intCount = 2
                    Case "����"
                        intCount = 3
                    Case "����"
                        intCount = 4
                    Case "����"
                        intCount = 5
                    Case "����"
                        intCount = 6
                    Case "����"
                        intCount = 7
                    Case "����"
                        intCount = 8
                    Case "����"
                        intCount = 9
                    Case "ʮ��"
                        intCount = 10
                    Case "����"
                        intCount = 11
                    Case "ʮ��"
                        intCount = 12
                End Select

                getChineseNumber = intCount

            Catch ex As Exception
            End Try

        End Function

        '��ȡ�·ݶ�Ӧ�ĺ��ֱ�ʾ
        Public Function getChineseMonth(ByVal strDigital As String) As String

            Dim strTemp(10) As String
            Dim intValue As Integer

            '��ʼ��
            strDigital = strDigital.Trim
            getChineseMonth = strDigital

            Try
                intValue = Me.getObjectValue(strDigital, 1)

                'ת��
                Select Case intValue
                    Case 1
                        strTemp(0) = "һ"
                    Case 2
                        strTemp(0) = "��"
                    Case 3
                        strTemp(0) = "��"
                    Case 4
                        strTemp(0) = "��"
                    Case 5
                        strTemp(0) = "��"
                    Case 6
                        strTemp(0) = "��"
                    Case 7
                        strTemp(0) = "��"
                    Case 8
                        strTemp(0) = "��"
                    Case 9
                        strTemp(0) = "��"
                    Case 10
                        strTemp(0) = "ʮ"
                    Case 11
                        strTemp(0) = "ʮһ"
                    Case 12
                        strTemp(0) = "ʮ��"
                End Select

            Catch ex As Exception

            End Try

            getChineseMonth = strTemp(0)

        End Function

        '��ȡ������Ӧ�ĺ��ֱ�ʾ
        Public Function getChineseDay(ByVal strDigital As String) As String

            Dim strTemp(10) As String
            Dim intValue As Integer

            '��ʼ��
            strDigital = Trim(strDigital)
            getChineseDay = strDigital

            Try
                intValue = Me.getObjectValue(strDigital, 1)

                'ת��
                Select Case intValue
                    Case 1
                        strTemp(0) = "һ"
                    Case 2
                        strTemp(0) = "��"
                    Case 3
                        strTemp(0) = "��"
                    Case 4
                        strTemp(0) = "��"
                    Case 5
                        strTemp(0) = "��"
                    Case 6
                        strTemp(0) = "��"
                    Case 7
                        strTemp(0) = "��"
                    Case 8
                        strTemp(0) = "��"
                    Case 9
                        strTemp(0) = "��"
                    Case 10
                        strTemp(0) = "ʮ"
                    Case 11
                        strTemp(0) = "ʮһ"
                    Case 12
                        strTemp(0) = "ʮ��"
                    Case 13
                        strTemp(0) = "ʮ��"
                    Case 14
                        strTemp(0) = "ʮ��"
                    Case 15
                        strTemp(0) = "ʮ��"
                    Case 16
                        strTemp(0) = "ʮ��"
                    Case 17
                        strTemp(0) = "ʮ��"
                    Case 18
                        strTemp(0) = "ʮ��"
                    Case 19
                        strTemp(0) = "ʮ��"
                    Case 20
                        strTemp(0) = "��ʮ"
                    Case 21
                        strTemp(0) = "��ʮһ"
                    Case 22
                        strTemp(0) = "��ʮ��"
                    Case 23
                        strTemp(0) = "��ʮ��"
                    Case 24
                        strTemp(0) = "��ʮ��"
                    Case 25
                        strTemp(0) = "��ʮ��"
                    Case 26
                        strTemp(0) = "��ʮ��"
                    Case 27
                        strTemp(0) = "��ʮ��"
                    Case 28
                        strTemp(0) = "��ʮ��"
                    Case 29
                        strTemp(0) = "��ʮ��"
                    Case 30
                        strTemp(0) = "��ʮ"
                    Case 31
                        strTemp(0) = "��ʮһ"
                End Select

                getChineseDay = strTemp(0)

            Catch ex As Exception
            End Try

        End Function

        '��ȡ���ڶ�Ӧ�ĺ��ֱ�ʾ
        Public Function getChineseDate(ByVal objDate As System.DateTime) As String

            Dim strTemp(10) As String

            getChineseDate = ""

            Try

                strTemp(0) = objDate.Year.ToString
                If strTemp(0) = "" Then
                    Exit Try
                End If
                strTemp(1) = getChineseNumber(strTemp(0))

                strTemp(2) = objDate.Month.ToString
                strTemp(3) = getChineseMonth(strTemp(2))

                strTemp(4) = objDate.Day.ToString
                strTemp(5) = getChineseDay(strTemp(4))

                getChineseDate = strTemp(1) + "��" + strTemp(3) + "��" + strTemp(5) + "��"

            Catch ex As Exception
            End Try

        End Function



        '----------------------------------------------------------------
        ' ����ַ�����������
        ' ����SearchConfig��������
        '     = True ������λ��ƥ��
        '     = False����ͷƥ��
        '----------------------------------------------------------------
        Public Function getNewSearchString(ByVal strValue As String) As String

            If strValue Is Nothing Then strValue = ""
            Try
                If strValue = "" Then
                    getNewSearchString = strValue
                Else
                    If strValue.Substring(0, 1) = "%" Then
                        getNewSearchString = strValue
                    Else
                        If SearchConfig = True Then
                            getNewSearchString = "%" + strValue
                        Else
                            getNewSearchString = strValue
                        End If
                    End If
                End If
            Catch ex As Exception
                getNewSearchString = strValue
            End Try

        End Function

    End Class 'PulicParameters

End Namespace 'Xydc.Platform.Common.Utilities
