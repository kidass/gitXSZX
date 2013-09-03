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
    ' 命名空间：Xydc.Platform.Common.Utilities
    ' 类名    ：BaseURI
    '
    ' 功能描述： 
    '     定义与Uri相关的操作
    '----------------------------------------------------------------
    Public Class BaseURI
        Implements IDisposable

        '缺省Url路径分隔符
        Public Const DEFAULT_DIRSEP As String = "/"








        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()
        End Sub

        '----------------------------------------------------------------
        ' 虚拟析构函数
        '----------------------------------------------------------------
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
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
        ' 从Uri中获取路径的目录层次(文件不算目录级别)
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
        ' 从Uri中获取路径(不含文件名)：不带根路径符
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
        ' 从Uri中获取全路径(不含文件名)：带根路径符
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
        ' 从Uri中获取到指定级别intLevel的全路径(不含文件名)：不带根路径符
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
        ' 从Uri中获取到指定级别intLevel的本级路径名
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
        ' 从Uri中获取文件名
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
        ' 合成Uri绝对路径AbsolutePath
        '----------------------------------------------------------------
        Public Function getAbsolutePath( _
            ByVal strScheme As String, _
            ByVal strHost As String, _
            ByVal intPort As Integer, _
            ByVal strPath As String) As String

            Try
                If strPath = "" Then
                    '根目录
                    getAbsolutePath = strScheme + "://" + strHost + ":" + intPort.ToString() + "/"
                Else
                    getAbsolutePath = strScheme + "://" + strHost + ":" + intPort.ToString() + "/" + strPath + "/"
                End If
            Catch ex As Exception
                getAbsolutePath = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 合成Uri绝对路径AbsolutePath
        '----------------------------------------------------------------
        Public Function getAbsolutePath( _
            ByVal strScheme As String, _
            ByVal strHost As String, _
            ByVal intPort As Integer, _
            ByVal strPath As String, _
            ByVal strFile As String) As String

            Try
                If strPath = "" Then
                    '根目录下文件
                    getAbsolutePath = strScheme + "://" + strHost + ":" + intPort.ToString() + "/" + strFile
                Else
                    getAbsolutePath = strScheme + "://" + strHost + ":" + intPort.ToString() + "/" + strPath + "/" + strFile
                End If
            Catch ex As Exception
                getAbsolutePath = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 将FTP样式的目录表示转化为本地文件样式的目录表示
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