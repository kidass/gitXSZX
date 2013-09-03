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
Imports System.Data
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Utilities

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.Common.Utilities
    ' ����    ��BaseLocalFile
    '
    ' ���������� 
    '     �����뱾���ļ���صĲ���
    '----------------------------------------------------------------
    Public Class BaseLocalFile
        Implements IDisposable

        'ȱʡĿ¼�ָ���
        Public Const DEFAULT_DIRSEP As String = "\"








        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' ������������
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' �ͷű�����Դ
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Utilities.BaseLocalFile)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��strFileSpec�л�ȡ·����Ŀ¼���(�ļ�����Ŀ¼����)
        '     strFileSpec      ���ļ�·����Ϣ
        ' ����
        '                      ��Ŀ¼������
        '----------------------------------------------------------------
        Public Function getPathLevel(ByVal strFileSpec As String) As Integer

            Try
                strFileSpec = System.IO.Path.GetFullPath(strFileSpec)
                Dim strPath As String = System.IO.Path.GetDirectoryName(strFileSpec)
                Dim strValue() As String = strPath.Split(System.IO.Path.DirectorySeparatorChar)
                getPathLevel = strValue.Length
            Catch ex As Exception
                getPathLevel = 0
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strFileSpec�л�ȡ��·��
        '     strFileSpec      ���ļ�·����Ϣ
        ' ����
        '                      ����Ŀ¼��Ϣ
        '----------------------------------------------------------------
        Public Function getPathRoot(ByVal strFileSpec As String) As String

            Try
                strFileSpec = System.IO.Path.GetFullPath(strFileSpec)
                getPathRoot = System.IO.Path.GetPathRoot(strFileSpec)
            Catch ex As Exception
                getPathRoot = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strFileSpec�л�ȡ·��(�����ļ���)
        '     strFileSpec      ���ļ�·����Ϣ
        ' ����
        '                      ����Ŀ¼��Ϣ
        '----------------------------------------------------------------
        Public Function getPathName(ByVal strFileSpec As String) As String

            Try
                strFileSpec = System.IO.Path.GetFullPath(strFileSpec)
                Dim strPath As String = System.IO.Path.GetDirectoryName(strFileSpec)
                Dim strSep As String = System.IO.Path.DirectorySeparatorChar
                If strPath <> "" Then
                    getPathName = strPath + strSep
                Else
                    getPathName = strPath
                End If
            Catch ex As Exception
                getPathName = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strFileSpec�л�ȡ��ָ������intLevel��ȫ·��(�����ļ���)
        '     strFileSpec      ���ļ�·����Ϣ
        '     intLevel         ������(��������Ϊ1)
        ' ����
        '                      ��������ָ�������Ŀ¼��
        '----------------------------------------------------------------
        Public Function getPathName( _
            ByVal strFileSpec As String, _
            ByVal intLevel As Integer) As String

            getPathName = ""
            Try
                strFileSpec = System.IO.Path.GetFullPath(strFileSpec)
                Dim strSep As String = System.IO.Path.DirectorySeparatorChar
                Dim strPath As String = System.IO.Path.GetDirectoryName(strFileSpec)
                Dim strValue() As String = strPath.Split(System.IO.Path.DirectorySeparatorChar)

                Dim strPathTemp As String = ""
                Dim intCount As Integer
                Dim i As Integer
                intCount = strValue.Length
                If intCount < intLevel Then Exit Try
                For i = 0 To intLevel - 1 Step 1
                    If strPathTemp = "" Then
                        strPathTemp = strValue(i)
                    Else
                        strPathTemp = strPathTemp + strSep + strValue(i)
                    End If
                Next
                If strPathTemp <> "" Then
                    getPathName = strPathTemp + strSep
                Else
                    getPathName = strPathTemp
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strFileSpec�л�ȡ��ָ������intLevel�ı���·����
        '     strFileSpec      ���ļ�·����Ϣ
        '     intLevel         ������(��������Ϊ1)
        '     blnUnused        ��������
        ' ����
        '                      ��ָ�������Ŀ¼��
        '----------------------------------------------------------------
        Public Function getPathName( _
            ByVal strFileSpec As String, _
            ByVal intLevel As Integer, _
            ByVal blnUnused As Boolean) As String

            getPathName = ""
            Try
                strFileSpec = System.IO.Path.GetFullPath(strFileSpec)
                Dim strPath As String = System.IO.Path.GetDirectoryName(strFileSpec)
                Dim strValue() As String = strPath.Split(System.IO.Path.DirectorySeparatorChar)

                Dim intCount As Integer
                intCount = strValue.Length
                If intCount < intLevel Then Exit Try
                getPathName = strValue(intLevel - 1)
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strFileSpec�л�ȡ�ļ���(����չ��)
        '     strFileSpec      ���ļ�·����Ϣ
        ' ����
        '                      ���ļ���(����չ��)
        '----------------------------------------------------------------
        Public Function getFileName(ByVal strFileSpec As String) As String

            Try
                getFileName = System.IO.Path.GetFileName(strFileSpec)
            Catch ex As Exception
                getFileName = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strFileSpec�л�ȡ�ļ���(������չ��)
        '     strFileSpec      ���ļ�·����Ϣ
        ' ����
        '                      ���ļ���(������չ��)
        '----------------------------------------------------------------
        Public Function getFileNameWithoutExtension(ByVal strFileSpec As String) As String

            Try
                getFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(strFileSpec)
            Catch ex As Exception
                getFileNameWithoutExtension = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��strFileSpec�л�ȡ�ļ���չ��(.xxx)
        '     strFileSpec      ���ļ�·����Ϣ
        ' ����
        '                      ���ļ���չ��
        '----------------------------------------------------------------
        Public Function getExtension(ByVal strFileSpec As String) As String

            Try
                getExtension = System.IO.Path.GetExtension(strFileSpec)
            Catch ex As Exception
                getExtension = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ����strFileSpecĿ¼Ҫ���Զ�����Ŀ¼
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strFileSpec      ��Ҫ����Ŀ¼��·����Ϣ
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doCreateDirectory( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

            doCreateDirectory = False
            strErrMsg = ""

            Try
                '��ȡĿ¼����
                Dim intLevel As Integer
                intLevel = getPathLevel(strFileSpec)

                '��ȷ��
                Dim strPath As String
                Dim i As Integer
                For i = 1 To intLevel Step 1
                    strPath = getPathName(strFileSpec, i)
                    If System.IO.Directory.Exists(strPath) = False Then
                        System.IO.Directory.CreateDirectory(strPath)
                        System.Threading.Thread.Sleep(15)
                    End If
                Next
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doCreateDirectory = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ʱ�ļ�
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strExtName       ����ʱ�ļ���չ��
        '     strFileName      ����ʱ�ļ�����(����)
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doCreateTempFile( _
            ByRef strErrMsg As String, _
            ByVal strExtName As String, _
            ByRef strFileName As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters

            doCreateTempFile = False
            strFileName = ""
            strErrMsg = ""

            Try

                'Dim strTempFileName As String = System.IO.Path.GetTempFileName()
                Dim strTempFileName As String = objPulicParameters.getNewGuid()

                Dim strName As String

                'strName = getFileNameWithoutExtension(strTempFileName)
                strName = strTempFileName

                If strExtName <> "" Then
                    strFileName = strName + strExtName
                Else
                    strFileName = strName
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doCreateTempFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ʱ�ļ�
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strRefFileSpec   ���ο��ļ�����·��(local or ftp)
        '     blnByRefFile     ��������
        '     strFileName      ����ʱ�ļ�����(����)
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doCreateTempFile( _
            ByRef strErrMsg As String, _
            ByVal strRefFileSpec As String, _
            ByVal blnByRefFile As Boolean, _
            ByRef strFileName As String) As Boolean

            Dim objPulicParameters As New Xydc.Platform.Common.Utilities.PulicParameters
            Dim strExtName As String = ""

            doCreateTempFile = False
            strFileName = ""
            strErrMsg = ""

            Try
                '��ȡԴ�ļ�����չ��
                If strRefFileSpec Is Nothing Then strRefFileSpec = ""
                strRefFileSpec = strRefFileSpec.Trim()
                If strRefFileSpec <> "" Then
                    strRefFileSpec = strRefFileSpec.Replace(Xydc.Platform.Common.Utilities.FTPProperty.DEFAULT_DIRSEP, System.IO.Path.DirectorySeparatorChar)
                    strExtName = getExtension(strRefFileSpec)
                End If

                '������ʱ�ļ�

                'Dim strTempFileName As String = System.IO.Path.GetTempFileName()
                Dim strTempFileName As String = objPulicParameters.getNewGuid()


                '��ȡ��ʱ�ļ�ȫ��

                Dim strName As String = ""
                'strName = getFileNameWithoutExtension(strTempFileName)
                strName = strTempFileName

                If strExtName <> "" Then
                    strFileName = strName + strExtName
                Else
                    strFileName = strName
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)

            doCreateTempFile = True
            Exit Function
errProc:
            Xydc.Platform.Common.Utilities.PulicParameters.SafeRelease(objPulicParameters)
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ʱ�ļ�
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strExtName       ����ʱ�ļ���չ��
        '     strDesPath       ����ʱ�ļ����·��
        '     strFullPath      ����ʱ�ļ�����·��(����)
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doCreateTempFile( _
            ByRef strErrMsg As String, _
            ByVal strExtName As String, _
            ByVal strDesPath As String, _
            ByRef strFullPath As String) As Boolean

            doCreateTempFile = False
            strFullPath = ""
            strErrMsg = ""

            Try
                '��ȡ�ļ���
                Dim strFileName As String
                If doCreateTempFile(strErrMsg, strExtName, strFileName) = False Then
                    Exit Try
                End If

                '��ȡ�ļ�·��
                strDesPath = getPathName(strDesPath)

                '����
                strFullPath = strDesPath + strFileName
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doCreateTempFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ������ʱ�ļ�
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strRefFileSpec   ���ο��ļ�����·��(local or ftp)
        '     blnByRefFile     ��������
        '     strDesPath       ����ʱ�ļ����·��
        '     strFullPath      ����ʱ�ļ�����·��(����)
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doCreateTempFile( _
            ByRef strErrMsg As String, _
            ByVal strRefFileSpec As String, _
            ByVal blnByRefFile As Boolean, _
            ByVal strDesPath As String, _
            ByRef strFullPath As String) As Boolean

            doCreateTempFile = False
            strFullPath = ""
            strErrMsg = ""

            Try
                '��ȡԴ�ļ�����չ��
                Dim strExtName As String = ""
                If strRefFileSpec Is Nothing Then strRefFileSpec = ""
                strRefFileSpec = strRefFileSpec.Trim()
                If strRefFileSpec <> "" Then
                    strRefFileSpec = strRefFileSpec.Replace(Xydc.Platform.Common.Utilities.FTPProperty.DEFAULT_DIRSEP, System.IO.Path.DirectorySeparatorChar)
                    strExtName = getExtension(strRefFileSpec)
                End If

                '��ȡ�ļ���
                Dim strFileName As String
                If doCreateTempFile(strErrMsg, strExtName, strFileName) = False Then
                    Exit Try
                End If

                '��ȡ�ļ�·��
                strDesPath = getPathName(strDesPath)

                '����
                strFullPath = strDesPath + strFileName
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doCreateTempFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ���ļ�·�����ļ����ϲ�������·��
        '     strFilePath      ���ļ�·��
        '     strFileName      ���ļ���
        ' ����
        '                      ������·��
        '----------------------------------------------------------------
        Public Function doMakePath( _
            ByVal strFilePath As String, _
            ByVal strFileName As String) As String

            doMakePath = ""

            Try
                Dim strSep As String = System.IO.Path.DirectorySeparatorChar
                Dim blnHasSep As Boolean = False
                If strFilePath.Substring(strFilePath.Length - 1, 1) = strSep Then
                    blnHasSep = True
                Else
                    If strFileName.Substring(0, 1) = strSep Then
                        blnHasSep = True
                    End If
                End If

                If blnHasSep = True Then
                    doMakePath = strFilePath + strFileName
                Else
                    doMakePath = strFilePath + strSep + strFileName
                End If
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��Դ�ļ����Ƶ�Ŀ���ļ���
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strSrcFile       ��Դ�ļ�����·��
        '     strDesFile       ��Ŀ���ļ�����·��
        '     blnOverwrite     ���Ƿ񸲸�Ŀ���ļ�?
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doCopyFile( _
            ByRef strErrMsg As String, _
            ByVal strSrcFile As String, _
            ByVal strDesFile As String, _
            ByVal blnOverwrite As Boolean) As Boolean

            doCopyFile = False
            strErrMsg = ""

            Try
                System.IO.File.Copy(strSrcFile, strDesFile, blnOverwrite)
                System.Threading.Thread.Sleep(15)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doCopyFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ָ���ļ����Ƶ�ָ��Ŀ¼�µ���ʱ�ļ���
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strSrcFile       ��Դ�ļ�����·��
        '     strDesPath       ��Ŀ���ļ��Ĵ�·��
        '     strDesFile       ��Ŀ���ļ���(����)
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doCopyToTempFile( _
            ByRef strErrMsg As String, _
            ByVal strSrcFile As String, _
            ByVal strDesPath As String, _
            ByRef strDesFile As String) As Boolean

            doCopyToTempFile = False
            strDesFile = ""
            strErrMsg = ""

            Try
                '��ȡ��ʱ�ļ���
                Dim strTempFile As String
                If Me.doCreateTempFile(strErrMsg, strSrcFile, True, strTempFile) = False Then
                    Exit Try
                End If

                '�����ļ�
                Dim strDesFilePath As String
                strDesFilePath = Me.doMakePath(strDesPath, strTempFile)
                If Me.doCopyFile(strErrMsg, strSrcFile, strDesFilePath, True) = False Then
                    GoTo errProc
                End If

                '������ʱ�ļ���
                strDesFile = strTempFile
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doCopyToTempFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ��ָ���ļ����Ƶ�ָ��Ŀ¼�µ���ʱ�ļ���
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strFileSpec      ��Ҫ�����ļ�·��
        '     blnExisted       ���Ƿ����(����)
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doFileExisted( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String, _
            ByRef blnExisted As Boolean) As Boolean

            doFileExisted = False
            blnExisted = True

            Try
                blnExisted = System.IO.File.Exists(strFileSpec)
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doFileExisted = True
            Exit Function
errProc:
            Exit Function
        End Function

        '----------------------------------------------------------------
        ' ɾ��ָ���ļ�
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strFileSpec      ��Ҫɾ�����ļ�·��
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doDeleteFile(ByRef strErrMsg As String, ByVal strFileSpec As String) As Boolean

            doDeleteFile = False

            Try
                Dim blnExisted As Boolean
                If Me.doFileExisted(strErrMsg, strFileSpec, blnExisted) = False Then
                    GoTo errProc
                End If

                If blnExisted = True Then
                    System.IO.File.Delete(strFileSpec)
                    System.Threading.Thread.Sleep(15)
                End If
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doDeleteFile = True
            Exit Function
errProc:
            Exit Function

        End Function

        '----------------------------------------------------------------
        ' ����ָ���ļ��Ƿ�����
        '     strErrMsg        �����ش�����Ϣ(����)
        '     strFileSpec      ��Ҫ���Ե��ļ�·��
        '     blnTestOK        �����ز��Խ��
        ' ����
        '     True             ���ɹ�
        '     False            ��ʧ��
        '----------------------------------------------------------------
        Public Function doTestFile( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String, _
            ByRef blnTestOK As Boolean) As Boolean

            doTestFile = False
            blnTestOK = False

            Try
                Dim blnExisted As Boolean
                If Me.doFileExisted(strErrMsg, strFileSpec, blnExisted) = False Then
                    GoTo errProc
                End If
                blnTestOK = blnExisted
            Catch ex As Exception
                strErrMsg = ex.Message
                GoTo errProc
            End Try

            doTestFile = True
            Exit Function
errProc:
            Exit Function

        End Function

    End Class

End Namespace