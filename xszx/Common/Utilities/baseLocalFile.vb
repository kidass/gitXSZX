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
    ' 命名空间：Xydc.Platform.Common.Utilities
    ' 类名    ：BaseLocalFile
    '
    ' 功能描述： 
    '     定义与本地文件相关的操作
    '----------------------------------------------------------------
    Public Class BaseLocalFile
        Implements IDisposable

        '缺省目录分隔符
        Public Const DEFAULT_DIRSEP As String = "\"








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
        ' 从strFileSpec中获取路径的目录层次(文件不算目录级别)
        '     strFileSpec      ：文件路径信息
        ' 返回
        '                      ：目录级别数
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
        ' 从strFileSpec中获取根路径
        '     strFileSpec      ：文件路径信息
        ' 返回
        '                      ：根目录信息
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
        ' 从strFileSpec中获取路径(不含文件名)
        '     strFileSpec      ：文件路径信息
        ' 返回
        '                      ：纯目录信息
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
        ' 从strFileSpec中获取到指定级别intLevel的全路径(不含文件名)
        '     strFileSpec      ：文件路径信息
        '     intLevel         ：级别(顶级级别为1)
        ' 返回
        '                      ：顶级到指定级别的目录名
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
        ' 从strFileSpec中获取到指定级别intLevel的本级路径名
        '     strFileSpec      ：文件路径信息
        '     intLevel         ：级别(顶级级别为1)
        '     blnUnused        ：重载用
        ' 返回
        '                      ：指定级别的目录名
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
        ' 从strFileSpec中获取文件名(带扩展名)
        '     strFileSpec      ：文件路径信息
        ' 返回
        '                      ：文件名(带扩展名)
        '----------------------------------------------------------------
        Public Function getFileName(ByVal strFileSpec As String) As String

            Try
                getFileName = System.IO.Path.GetFileName(strFileSpec)
            Catch ex As Exception
                getFileName = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 从strFileSpec中获取文件名(不带扩展名)
        '     strFileSpec      ：文件路径信息
        ' 返回
        '                      ：文件名(不带扩展名)
        '----------------------------------------------------------------
        Public Function getFileNameWithoutExtension(ByVal strFileSpec As String) As String

            Try
                getFileNameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(strFileSpec)
            Catch ex As Exception
                getFileNameWithoutExtension = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 从strFileSpec中获取文件扩展名(.xxx)
        '     strFileSpec      ：文件路径信息
        ' 返回
        '                      ：文件扩展名
        '----------------------------------------------------------------
        Public Function getExtension(ByVal strFileSpec As String) As String

            Try
                getExtension = System.IO.Path.GetExtension(strFileSpec)
            Catch ex As Exception
                getExtension = ""
            End Try

        End Function

        '----------------------------------------------------------------
        ' 根据strFileSpec目录要求自动创建目录
        '     strErrMsg        ：返回错误信息(返回)
        '     strFileSpec      ：要创建目录的路径信息
        ' 返回
        '     True             ：成功
        '     False            ：失败
        '----------------------------------------------------------------
        Public Function doCreateDirectory( _
            ByRef strErrMsg As String, _
            ByVal strFileSpec As String) As Boolean

            doCreateDirectory = False
            strErrMsg = ""

            Try
                '获取目录级别
                Dim intLevel As Integer
                intLevel = getPathLevel(strFileSpec)

                '逐级确认
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
        ' 创建临时文件
        '     strErrMsg        ：返回错误信息(返回)
        '     strExtName       ：临时文件扩展名
        '     strFileName      ：临时文件名称(返回)
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
        ' 创建临时文件
        '     strErrMsg        ：返回错误信息(返回)
        '     strRefFileSpec   ：参考文件完整路径(local or ftp)
        '     blnByRefFile     ：重载用
        '     strFileName      ：临时文件名称(返回)
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '获取源文件的扩展名
                If strRefFileSpec Is Nothing Then strRefFileSpec = ""
                strRefFileSpec = strRefFileSpec.Trim()
                If strRefFileSpec <> "" Then
                    strRefFileSpec = strRefFileSpec.Replace(Xydc.Platform.Common.Utilities.FTPProperty.DEFAULT_DIRSEP, System.IO.Path.DirectorySeparatorChar)
                    strExtName = getExtension(strRefFileSpec)
                End If

                '创建临时文件

                'Dim strTempFileName As String = System.IO.Path.GetTempFileName()
                Dim strTempFileName As String = objPulicParameters.getNewGuid()


                '获取临时文件全名

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
        ' 创建临时文件
        '     strErrMsg        ：返回错误信息(返回)
        '     strExtName       ：临时文件扩展名
        '     strDesPath       ：临时文件存放路径
        '     strFullPath      ：临时文件完整路径(返回)
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '获取文件名
                Dim strFileName As String
                If doCreateTempFile(strErrMsg, strExtName, strFileName) = False Then
                    Exit Try
                End If

                '获取文件路径
                strDesPath = getPathName(strDesPath)

                '复合
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
        ' 创建临时文件
        '     strErrMsg        ：返回错误信息(返回)
        '     strRefFileSpec   ：参考文件完整路径(local or ftp)
        '     blnByRefFile     ：重载用
        '     strDesPath       ：临时文件存放路径
        '     strFullPath      ：临时文件完整路径(返回)
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '获取源文件的扩展名
                Dim strExtName As String = ""
                If strRefFileSpec Is Nothing Then strRefFileSpec = ""
                strRefFileSpec = strRefFileSpec.Trim()
                If strRefFileSpec <> "" Then
                    strRefFileSpec = strRefFileSpec.Replace(Xydc.Platform.Common.Utilities.FTPProperty.DEFAULT_DIRSEP, System.IO.Path.DirectorySeparatorChar)
                    strExtName = getExtension(strRefFileSpec)
                End If

                '获取文件名
                Dim strFileName As String
                If doCreateTempFile(strErrMsg, strExtName, strFileName) = False Then
                    Exit Try
                End If

                '获取文件路径
                strDesPath = getPathName(strDesPath)

                '复合
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
        ' 将文件路径、文件名合并成完整路径
        '     strFilePath      ：文件路径
        '     strFileName      ：文件名
        ' 返回
        '                      ：完整路径
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
        ' 将源文件复制到目标文件中
        '     strErrMsg        ：返回错误信息(返回)
        '     strSrcFile       ：源文件完整路径
        '     strDesFile       ：目标文件完整路径
        '     blnOverwrite     ：是否覆盖目标文件?
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
        ' 将指定文件复制到指定目录下的临时文件中
        '     strErrMsg        ：返回错误信息(返回)
        '     strSrcFile       ：源文件完整路径
        '     strDesPath       ：目标文件的纯路径
        '     strDesFile       ：目标文件名(返回)
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
                '获取临时文件名
                Dim strTempFile As String
                If Me.doCreateTempFile(strErrMsg, strSrcFile, True, strTempFile) = False Then
                    Exit Try
                End If

                '复制文件
                Dim strDesFilePath As String
                strDesFilePath = Me.doMakePath(strDesPath, strTempFile)
                If Me.doCopyFile(strErrMsg, strSrcFile, strDesFilePath, True) = False Then
                    GoTo errProc
                End If

                '返回临时文件名
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
        ' 将指定文件复制到指定目录下的临时文件中
        '     strErrMsg        ：返回错误信息(返回)
        '     strFileSpec      ：要检查的文件路径
        '     blnExisted       ：是否存在(返回)
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
        ' 删除指定文件
        '     strErrMsg        ：返回错误信息(返回)
        '     strFileSpec      ：要删除的文件路径
        ' 返回
        '     True             ：成功
        '     False            ：失败
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
        ' 测试指定文件是否正常
        '     strErrMsg        ：返回错误信息(返回)
        '     strFileSpec      ：要测试的文件路径
        '     blnTestOK        ：返回测试结果
        ' 返回
        '     True             ：成功
        '     False            ：失败
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