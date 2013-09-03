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
Imports System.Net
Imports System.Data
Imports System.Security
Imports Xydc.Net.FtpWebRequest
Imports Xydc.Net.FtpWebResponse
Imports System.Runtime.Serialization

Namespace Xydc.Platform.Common.Utilities

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.Common.Utilities
    ' ����    ��ResourceManager
    '
    ' ���������� 
    '     ͨ��Ӧ����Դ���չ���
    '----------------------------------------------------------------
    Public Class ResourceManager
        Implements IDisposable








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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.Common.Utilities.ResourceManager)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub








        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.IO.FileStream
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.IO.StreamWriter)
            Try
                If Not (obj Is Nothing) Then
                    obj.Close()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.IO.FileStream
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.IO.FileStream)
            Try
                If Not (obj Is Nothing) Then
                    obj.Close()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.IO.Stream
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.IO.Stream)
            Try
                If Not (obj Is Nothing) Then
                    obj.Close()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.IO.MemoryStream
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.IO.MemoryStream)
            Try
                If Not (obj Is Nothing) Then
                    obj.Close()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.IO.FileInfo
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.IO.FileInfo)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��ȫ�ͷ�Josco.Net.FtpWebResponse
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Net.FtpWebResponse)
            Try
                If Not (obj Is Nothing) Then
                    obj.Close()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�Josco.Net.FtpWebRequest
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Net.FtpWebRequest)
            Try
                If Not (obj Is Nothing) Then
                    obj.Close()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Net.WebResponse
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Net.WebResponse)
            Try
                If Not (obj Is Nothing) Then
                    obj.Close()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Net.WebRequest
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Net.WebRequest)
            Try
                If Not (obj Is Nothing) Then
                    obj = Nothing
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub









        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Security.Cryptography.CryptoStream
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Security.Cryptography.CryptoStream)
            Try
                If Not (obj Is Nothing) Then
                    obj.Close()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Security.Cryptography.ICryptoTransform
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Security.Cryptography.ICryptoTransform)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Security.Cryptography.RijndaelManaged
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Security.Cryptography.RijndaelManaged)
            Try
                If Not (obj Is Nothing) Then
                    'obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub







        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Collections.Specialized.NameValueCollection
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Collections.Specialized.NameValueCollection)

            Try
                If Not (obj Is Nothing) Then
                    obj.Clear()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Collections.Specialized.ListDictionary
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Collections.Specialized.ListDictionary)

            Try
                If Not (obj Is Nothing) Then
                    obj.Clear()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Collections.ArrayList
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Collections.ArrayList)

            Try
                If Not (obj Is Nothing) Then
                    obj.Clear()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub









        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Data.SqlClient.SqlConnection
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Data.SqlClient.SqlDataAdapter)

            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub

        Public Shared Sub SafeRelease(ByRef obj As System.Data.SqlClient.SqlConnection)

            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Data.SqlClient.SqlCommand
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Data.SqlClient.SqlCommand)

            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Data.DataSet
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Data.DataSet)

            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Data.DataTable
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Data.DataTable)

            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub







        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Data.Odbc.OdbcCommand
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Data.Odbc.OdbcCommand)

            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Data.Odbc.OdbcCommand
        '----------------------------------------------------------------
        Public Shared Sub SafeRelease(ByRef obj As System.Data.Odbc.OdbcConnection)

            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub





        '----------------------------------------------------------------
        ' ��ȫ�ͷ�System.Uri
        '----------------------------------------------------------------

        Public Shared Sub SafeRelease(ByRef obj As System.Uri)

            Try
                If Not (obj Is Nothing) Then
                    obj = Nothing
                End If
            Catch ex As Exception
            End Try
            obj = Nothing

        End Sub


    End Class

End Namespace