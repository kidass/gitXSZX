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

Namespace Xydc.Platform.Common.Utilities

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.Common.Utilities
    ' ����    ��BaseURI
    '
    ' ���������� 
    '     ������Uri��صĲ���
    '----------------------------------------------------------------
    Public Class BaseURI
        Implements IDisposable

        'ȱʡUrl·���ָ���
        Public Const DEFAULT_DIRSEP As String = "/"








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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Utilities.BaseURI)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub










        '----------------------------------------------------------------
        ' ��Uri�л�ȡ·����Ŀ¼���(�ļ�����Ŀ¼����)
        '----------------------------------------------------------------
        Public Function getPathLevel(ByVal strUri As String) As Integer

            getPathLevel = 0
            Try
                Dim objUri As New System.Uri(strUri)
                Dim strValue() As String
                strValue = objUri.AbsolutePath.Split(DEFAULT_DIRSEP.ToCharArray())
                getPathLevel = strValue.Length - 1
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��Uri�л�ȡ·��(�����ļ���)��������·����
        '----------------------------------------------------------------
        Public Function getPathName(ByVal strUri As String) As String

            getPathName = ""
            Try
                Dim objUri As New System.Uri(strUri)
                Dim strValue() As String
                strValue = objUri.AbsolutePath.Split(DEFAULT_DIRSEP.ToCharArray())

                Dim intCount As Integer
                Dim i As Integer
                intCount = strValue.Length
                For i = 0 To intCount - 2 Step 1
                    If strValue(i) <> "" Then
                        If getPathName = "" Then
                            getPathName = strValue(i)
                        Else
                            getPathName = getPathName + DEFAULT_DIRSEP + strValue(i)
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��Uri�л�ȡȫ·��(�����ļ���)������·����
        '----------------------------------------------------------------
        Public Function getPathName( _
            ByVal strUri As String, _
            ByVal blnWithRootSep As Boolean) As String

            getPathName = ""
            Try
                Dim objUri As New System.Uri(strUri)
                Dim strValue() As String
                strValue = objUri.AbsolutePath.Split(DEFAULT_DIRSEP.ToCharArray())

                Dim intCount As Integer
                Dim i As Integer
                intCount = strValue.Length
                For i = 0 To intCount - 2 Step 1
                    If strValue(i) <> "" Then
                        If getPathName = "" Then
                            getPathName = DEFAULT_DIRSEP + strValue(i)
                        Else
                            getPathName = getPathName + DEFAULT_DIRSEP + strValue(i)
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��Uri�л�ȡ��ָ������intLevel��ȫ·��(�����ļ���)��������·����
        '----------------------------------------------------------------
        Public Function getPathName( _
            ByVal strUri As String, _
            ByVal intLevel As Integer) As String

            getPathName = ""
            Try
                Dim objUri As New System.Uri(strUri)
                Dim strValue() As String
                strValue = objUri.AbsolutePath.Split(DEFAULT_DIRSEP.ToCharArray())

                Dim intCount As Integer
                Dim i As Integer
                intCount = strValue.Length
                If intCount < intLevel Then Exit Try
                For i = 0 To intLevel - 1 Step 1
                    If strValue(i) <> "" Then
                        If getPathName = "" Then
                            getPathName = strValue(i)
                        Else
                            getPathName = getPathName + DEFAULT_DIRSEP + strValue(i)
                        End If
                    End If
                Next
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��Uri�л�ȡ��ָ������intLevel�ı���·����
        '----------------------------------------------------------------
        Public Function getPathName( _
            ByVal strUri As String, _
            ByVal intLevel As Integer, _
            ByVal blnUnused As Boolean) As String

            getPathName = ""
            Try
                Dim objUri As New System.Uri(strUri)
                Dim strValue() As String
                strValue = objUri.AbsolutePath.Split(DEFAULT_DIRSEP.ToCharArray())

                Dim intCount As Integer
                intCount = strValue.Length
                If intCount < intLevel Then Exit Try

                getPathName = strValue(intLevel - 1)
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��Uri�л�ȡ�ļ���
        '----------------------------------------------------------------
        Public Function getFileName(ByVal strUri As String) As String

            getFileName = ""
            Try
                Dim objUri As New System.Uri(strUri)
                Dim strValue() As String
                strValue = objUri.AbsolutePath.Split(DEFAULT_DIRSEP.ToCharArray())

                Dim intCount As Integer
                intCount = strValue.Length
                getFileName = strValue(intCount - 1)
            Catch ex As Exception
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ϳ�Uri����·��AbsolutePath
        '----------------------------------------------------------------
        Public Function getAbsolutePath( _
            ByVal strScheme As String, _
            ByVal strHost As String, _
            ByVal intPort As Integer, _
            ByVal strPath As String) As String

            Try
                If strPath = "" Then
                    '��Ŀ¼
                    getAbsolutePath = strScheme + "://" + strHost + ":" + intPort.ToString() + "/"
                Else
                    getAbsolutePath = strScheme + "://" + strHost + ":" + intPort.ToString() + "/" + strPath + "/"
                End If
            Catch ex As Exception
                getAbsolutePath = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' �ϳ�Uri����·��AbsolutePath
        '----------------------------------------------------------------
        Public Function getAbsolutePath( _
            ByVal strScheme As String, _
            ByVal strHost As String, _
            ByVal intPort As Integer, _
            ByVal strPath As String, _
            ByVal strFile As String) As String

            Try
                If strPath = "" Then
                    '��Ŀ¼���ļ�
                    getAbsolutePath = strScheme + "://" + strHost + ":" + intPort.ToString() + "/" + strFile
                Else
                    getAbsolutePath = strScheme + "://" + strHost + ":" + intPort.ToString() + "/" + strPath + "/" + strFile
                End If
            Catch ex As Exception
                getAbsolutePath = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' ��FTP��ʽ��Ŀ¼��ʾת��Ϊ�����ļ���ʽ��Ŀ¼��ʾ
        '----------------------------------------------------------------
        Public Function doConvertToLocalPath(ByVal strUrlPath As String) As String

            doConvertToLocalPath = ""

            Try
                If Not (strUrlPath Is Nothing) Then
                    doConvertToLocalPath = strUrlPath.Replace(DEFAULT_DIRSEP, Xydc.Platform.Common.Utilities.BaseLocalFile.DEFAULT_DIRSEP)
                End If
            Catch ex As Exception
            End Try

        End Function

    End Class

End Namespace