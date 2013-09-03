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
Imports Microsoft.VisualBasic.FileSystem
Imports Microsoft.VisualBasic.Strings

Imports System
Imports System.IO
Imports System.Configuration
Imports System.Text
Imports System.Diagnostics
Imports System.Threading

Namespace Xydc.Platform.SystemFramework

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.SystemFramework
    ' 类名    ：ApplicationLog
    '
    ' Description:
    '   Logging class to provide tracing and logging support. There are
    '   four different logging levels (error, warning, info, trace) that
    '   produce output to either the system event log or a tracing file
    '   as specified in the current ApplicationConfiguration settings.
    '----------------------------------------------------------------
    Public Class ApplicationLog

        'TraceSwitch for changing which values are actually written to the trace output file.
        Private Shared debugSwitch As System.Diagnostics.TraceSwitch

        'This object is added as a debug listener.
        Private Shared accessWriter As System.IO.StreamWriter

        'This object is added as a audit listener.
        Private Shared auditPZWriter As System.IO.StreamWriter

        'This object is added as a audit listener.
        Private Shared auditAQWriter As System.IO.StreamWriter

        'This object is added as a audit listener.
        Private Shared auditSJWriter As System.IO.StreamWriter

        'EventLog variables used if the event log is specified
        Private Shared eventLogTraceLevel As System.Diagnostics.TraceLevel







        '----------------------------------------------------------------
        ' Shared Function FormatException:
        '   Write at the Verbose level to the event log and/or tracing file.
        ' Returns:
        '   A nicely format exception string, including message and StackTrace
        '     information.
        ' Parameters:
        '   [in] ex: The Exception object to format.
        '   [in] catchInfo: The string to prepend to the exception information.
        '----------------------------------------------------------------
        Public Shared Function FormatException(ByVal ex As Exception, Optional ByVal catchInfo As String = "") As String
            With New StringBuilder
                If Len(catchInfo) <> 0 Then .Append(catchInfo).Append(ControlChars.CrLf)
                FormatException = .Append(ex.Message).Append(ControlChars.CrLf).Append(ex.StackTrace).ToString
            End With
        End Function

        '----------------------------------------------------------------
        ' 从doSplitMessage解析出操作员和操作描述
        ' Parameters:
        '   [in ] strMessage: 准备提示信息
        '   [out] strUserId : 操作员
        '   [out] strInfo   : 操作描述
        '----------------------------------------------------------------
        Private Shared Function doSplitMessage(ByVal strMessage As String, ByRef strUserId As String, ByRef strInfo As String) As Boolean

            doSplitMessage = False
            strInfo = strMessage
            strUserId = ""

            Try
                '拆分
                Dim strChar() As Char
                strChar = strMessage.ToCharArray()

                '解析
                Dim intPos As Integer = -1
                Dim intCount As Integer
                Dim i As Integer
                intCount = strChar.Length
                For i = 0 To intCount - 1 Step 1
                    If strChar(i) = "]" Then
                        intPos = i
                        Exit For
                    End If
                Next

                '返回
                If intPos > 0 Then
                    strUserId = strMessage.Substring(0, intPos)
                    strInfo = strMessage.Substring(intPos + 1)
                    strUserId = strUserId.Replace("[", "")
                End If

            Catch ex As Exception
                strInfo = strMessage
                strUserId = ""
            End Try

            doSplitMessage = True
            Exit Function

        End Function


        '----------------------------------------------------------------
        ' 从doSplitMessage解析出操作员和操作描述
        ' Parameters:
        '   [in ] strMessage: 准备提示信息
        '   [out] strUserId : 操作员
        '   [out] strInfo   : 操作描述
        '----------------------------------------------------------------
        Private Shared Function doNormalizeXmlString(ByVal strMessage As String) As String

            Try
                strMessage = strMessage.Replace("&", "^")
            Catch ex As Exception
            End Try

            doNormalizeXmlString = strMessage
            Exit Function

        End Function
       






        '----------------------------------------------------------------
        ' Shared Sub WriteError:
        '   Write at the Error level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteError(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteLog(TraceLevel.Error, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteError:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteError(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <jsoalogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </jsoalogItem>"
            WriteLog(TraceLevel.Error, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteWarning:
        '   Write at the Warning level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteWarning(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteLog(TraceLevel.Warning, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteWarning:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteWarning(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <jsoalogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </jsoalogItem>"
            WriteLog(TraceLevel.Warning, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteInfo(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteLog(TraceLevel.Info, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteInfo(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <jsoalogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </jsoalogItem>"
            WriteLog(TraceLevel.Info, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteTrace:
        '   Write at the Verbose level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteTrace(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteLog(TraceLevel.Verbose, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteTrace(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <jsoalogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </jsoalogItem>"
            WriteLog(TraceLevel.Verbose, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteLog:
        '   Determine where a string needs to be written based on the
        '     configuration settings and the error level
        ' Parameters:
        '   [in] level: The severity of the information to be logged.
        '   [in] messageText: The string to be logged.
        '----------------------------------------------------------------
        Private Shared Sub WriteLog(ByVal level As TraceLevel, ByVal messageText As String)

            ' Be very careful by putting a Try/Catch around the entire routine.
            '   We should never throw an exception while logging.
            Try

                '
                ' Write the message to the trace file
                '
                If ApplicationConfiguration.TracingEnabled = False Then
                    Exit Try
                End If

                'Make sure a tracing file is specified.
                If Not (accessWriter Is Nothing) Then
                    'Log based on switch level.
                    If level <= debugSwitch.Level Then
                        messageText = messageText.Replace(vbCr, auditSJWriter.NewLine())
                        messageText += accessWriter.NewLine()
                        SyncLock accessWriter
                            accessWriter.Write(messageText)
                            accessWriter.Flush()
                        End SyncLock
                    End If
                End If

                ' Write the message to the system event log. We only write the message
                '   if the configuration settings say it is severe enough to warrant
                '   an entry in the event log.
                If level <= eventLogTraceLevel Then
                    ' Map the trace level to the corresponding event log attribute.
                    '   Note that EventLogEntryType = 2 ^ (level - 1), but it is generally not
                    '   considered good style to apply arithmetic operations to enum values.
                    Dim logEntryType As EventLogEntryType
                    Select Case level
                        Case TraceLevel.Error
                            logEntryType = EventLogEntryType.Error
                        Case TraceLevel.Warning
                            logEntryType = EventLogEntryType.Warning
                        Case TraceLevel.Info
                            logEntryType = EventLogEntryType.Information
                        Case TraceLevel.Verbose
                            logEntryType = EventLogEntryType.SuccessAudit
                    End Select

                    Dim eventLog As New EventLog("Application", ApplicationConfiguration.EventLogMachineName, ApplicationConfiguration.EventLogSourceName)

                    'Write the entry to the event log
                    eventLog.WriteEntry(messageText, logEntryType)
                End If
            Catch
                'Ignore any exceptions.
            End Try
        End Sub








        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZError:
        '   Write at the Error level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditPZError(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditPZLog(TraceLevel.Error, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZError:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditPZError(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditpzlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditpzlogItem>"
            WriteAuditPZLog(TraceLevel.Error, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZWarning:
        '   Write at the Warning level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditPZWarning(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditPZLog(TraceLevel.Warning, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZWarning:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditPZWarning(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditpzlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditpzlogItem>"
            WriteAuditPZLog(TraceLevel.Warning, strTemp)

        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditPZInfo(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditPZLog(TraceLevel.Info, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditPZInfo(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditpzlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditpzlogItem>"
            WriteAuditPZLog(TraceLevel.Info, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZTrace:
        '   Write at the Verbose level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditPZTrace(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditPZLog(TraceLevel.Verbose, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZTrace:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditPZTrace(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditpzlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditpzlogItem>"
            WriteAuditPZLog(TraceLevel.Verbose, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditPZLog:
        '   Determine where a string needs to be written based on the
        '     configuration settings and the error level
        ' Parameters:
        '   [in] level: The severity of the information to be logged.
        '   [in] messageText: The string to be logged.
        '----------------------------------------------------------------
        Private Shared Sub WriteAuditPZLog(ByVal level As TraceLevel, ByVal messageText As String)

            ' Be very careful by putting a Try/Catch around the entire routine.
            '   We should never throw an exception while logging.
            Try

                '
                ' Write the message to the trace file
                '
                If ApplicationConfiguration.TracingEnabled = False Then
                    Exit Try
                End If

                'Make sure a tracing file is specified.
                If Not (auditPZWriter Is Nothing) Then
                    'Log based on switch level.
                    If level <= debugSwitch.Level Then
                        messageText = messageText.Replace(vbCr, auditSJWriter.NewLine())
                        messageText += auditPZWriter.NewLine()
                        SyncLock auditPZWriter
                            auditPZWriter.Write(messageText)
                            auditPZWriter.Flush()
                        End SyncLock
                    End If
                End If

                ' Write the message to the system event log. We only write the message
                '   if the configuration settings say it is severe enough to warrant
                '   an entry in the event log.
                If level <= eventLogTraceLevel Then
                    ' Map the trace level to the corresponding event log attribute.
                    '   Note that EventLogEntryType = 2 ^ (level - 1), but it is generally not
                    '   considered good style to apply arithmetic operations to enum values.
                    Dim logEntryType As EventLogEntryType
                    Select Case level
                        Case TraceLevel.Error
                            logEntryType = EventLogEntryType.Error
                        Case TraceLevel.Warning
                            logEntryType = EventLogEntryType.Warning
                        Case TraceLevel.Info
                            logEntryType = EventLogEntryType.Information
                        Case TraceLevel.Verbose
                            logEntryType = EventLogEntryType.SuccessAudit
                    End Select

                    Dim eventLog As New EventLog("Application", ApplicationConfiguration.EventLogMachineName, ApplicationConfiguration.EventLogSourceName)

                    'Write the entry to the event log
                    eventLog.WriteEntry(messageText, logEntryType)
                End If
            Catch
                'Ignore any exceptions.
            End Try
        End Sub







        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQError:
        '   Write at the Error level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditAQError(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditAQLog(TraceLevel.Error, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQError:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditAQError(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditaqlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditaqlogItem>"
            WriteAuditAQLog(TraceLevel.Error, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQWarning:
        '   Write at the Warning level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditAQWarning(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditAQLog(TraceLevel.Warning, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQWarning:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditAQWarning(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditaqlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditaqlogItem>"
            WriteAuditAQLog(TraceLevel.Warning, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditAQInfo(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditAQLog(TraceLevel.Info, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditAQInfo(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditaqlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditaqlogItem>"
            WriteAuditAQLog(TraceLevel.Info, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQTrace:
        '   Write at the Verbose level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditAQTrace(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditAQLog(TraceLevel.Verbose, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQTrace:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditAQTrace(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditaqlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditaqlogItem>"
            WriteAuditAQLog(TraceLevel.Verbose, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditAQLog:
        '   Determine where a string needs to be written based on the
        '     configuration settings and the error level
        ' Parameters:
        '   [in] level: The severity of the information to be logged.
        '   [in] messageText: The string to be logged.
        '----------------------------------------------------------------
        Private Shared Sub WriteAuditAQLog(ByVal level As TraceLevel, ByVal messageText As String)

            ' Be very careful by putting a Try/Catch around the entire routine.
            '   We should never throw an exception while logging.
            Try

                '
                ' Write the message to the trace file
                '
                If ApplicationConfiguration.TracingEnabled = False Then
                    Exit Try
                End If

                'Make sure a tracing file is specified.
                If Not (auditAQWriter Is Nothing) Then
                    'Log based on switch level.
                    If level <= debugSwitch.Level Then
                        messageText = messageText.Replace(vbCr, auditSJWriter.NewLine())
                        messageText += auditAQWriter.NewLine()
                        SyncLock auditAQWriter
                            auditAQWriter.Write(messageText)
                            auditAQWriter.Flush()
                        End SyncLock
                    End If
                End If

                ' Write the message to the system event log. We only write the message
                '   if the configuration settings say it is severe enough to warrant
                '   an entry in the event log.
                If level <= eventLogTraceLevel Then
                    ' Map the trace level to the corresponding event log attribute.
                    '   Note that EventLogEntryType = 2 ^ (level - 1), but it is generally not
                    '   considered good style to apply arithmetic operations to enum values.
                    Dim logEntryType As EventLogEntryType
                    Select Case level
                        Case TraceLevel.Error
                            logEntryType = EventLogEntryType.Error
                        Case TraceLevel.Warning
                            logEntryType = EventLogEntryType.Warning
                        Case TraceLevel.Info
                            logEntryType = EventLogEntryType.Information
                        Case TraceLevel.Verbose
                            logEntryType = EventLogEntryType.SuccessAudit
                    End Select

                    Dim eventLog As New EventLog("Application", ApplicationConfiguration.EventLogMachineName, ApplicationConfiguration.EventLogSourceName)

                    'Write the entry to the event log
                    eventLog.WriteEntry(messageText, logEntryType)
                End If
            Catch
                'Ignore any exceptions.
            End Try
        End Sub








        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJError:
        '   Write at the Error level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditSJError(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditSJLog(TraceLevel.Error, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJError:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditSJError(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditsjlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditsjlogItem>"
            WriteAuditSJLog(TraceLevel.Error, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJWarning:
        '   Write at the Warning level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditSJWarning(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditSJLog(TraceLevel.Warning, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJWarning:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditSJWarning(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditsjlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditsjlogItem>"
            WriteAuditSJLog(TraceLevel.Warning, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditSJInfo(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditSJLog(TraceLevel.Info, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJInfo:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditSJInfo(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditsjlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditsjlogItem>"
            WriteAuditSJLog(TraceLevel.Info, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJTrace:
        '   Write at the Verbose level to the event log and/or tracing file.
        ' Parameters:
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditSJTrace(ByVal message As String)
            'Defer to the helper function to log the message.
            WriteAuditSJLog(TraceLevel.Verbose, message)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJTrace:
        '   Write at the Info level to the event log and/or tracing file.
        ' Parameters:
        '   [in] address: host address
        '   [in] message: The text to write to the log file or event log.
        '----------------------------------------------------------------
        Public Shared Sub WriteAuditSJTrace(ByVal address As String, ByVal machine As String, ByVal message As String)
            Dim strUserId As String = ""
            Dim strTemp As String = ""
            Dim strMsg As String = ""
            doSplitMessage(message, strUserId, strMsg)

            strMsg = doNormalizeXmlString(strMsg)

            strTemp = strTemp + "  <auditsjlogItem>" + vbCr
            strTemp = strTemp + "    <optime>" + Now.ToString("yyyy-MM-dd HH:mm:ss") + "</optime>" + vbCr
            strTemp = strTemp + "    <opaddr>" + address + "</opaddr>" + vbCr

            strTemp = strTemp + "    <opmach>" + machine + "</opmach>" + vbCr

            strTemp = strTemp + "    <opuser>" + strUserId + "</opuser>" + vbCr
            strTemp = strTemp + "    <opnote>" + strMsg + "</opnote>" + vbCr
            strTemp = strTemp + "  </auditsjlogItem>"
            WriteAuditSJLog(TraceLevel.Verbose, strTemp)
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub WriteAuditSJLog:
        '   Determine where a string needs to be written based on the
        '     configuration settings and the error level
        ' Parameters:
        '   [in] level: The severity of the information to be logged.
        '   [in] messageText: The string to be logged.
        '----------------------------------------------------------------
        Private Shared Sub WriteAuditSJLog(ByVal level As TraceLevel, ByVal messageText As String)

            ' Be very careful by putting a Try/Catch around the entire routine.
            '   We should never throw an exception while logging.
            Try

                '
                ' Write the message to the trace file
                '
                If ApplicationConfiguration.TracingEnabled = False Then
                    Exit Try
                End If

                'Make sure a tracing file is specified.
                If Not (auditSJWriter Is Nothing) Then
                    'Log based on switch level.
                    If level <= debugSwitch.Level Then
                        messageText = messageText.Replace(vbCr, auditSJWriter.NewLine())
                        messageText += auditSJWriter.NewLine()
                        SyncLock auditSJWriter
                            auditSJWriter.Write(messageText)
                            auditSJWriter.Flush()
                        End SyncLock
                    End If
                End If

                ' Write the message to the system event log. We only write the message
                '   if the configuration settings say it is severe enough to warrant
                '   an entry in the event log.
                If level <= eventLogTraceLevel Then
                    ' Map the trace level to the corresponding event log attribute.
                    '   Note that EventLogEntryType = 2 ^ (level - 1), but it is generally not
                    '   considered good style to apply arithmetic operations to enum values.
                    Dim logEntryType As EventLogEntryType
                    Select Case level
                        Case TraceLevel.Error
                            logEntryType = EventLogEntryType.Error
                        Case TraceLevel.Warning
                            logEntryType = EventLogEntryType.Warning
                        Case TraceLevel.Info
                            logEntryType = EventLogEntryType.Information
                        Case TraceLevel.Verbose
                            logEntryType = EventLogEntryType.SuccessAudit
                    End Select

                    Dim eventLog As New EventLog("Application", ApplicationConfiguration.EventLogMachineName, ApplicationConfiguration.EventLogSourceName)

                    'Write the entry to the event log
                    eventLog.WriteEntry(messageText, logEntryType)
                End If
            Catch
                'Ignore any exceptions.
            End Try
        End Sub








        '----------------------------------------------------------------
        ' Shared Sub New:
        '   Initialize all shared variables based on the ApplicationConfiguration
        '     settings. Called when this class is first loaded.
        '----------------------------------------------------------------
        Shared Sub New()

            ' Read the current settings from the configuration file to determine
            '   whether we need trace file support, event logging, or both.
            Dim myType As Type
            'Get the class object in order to take the initialization lock
            myType = GetType(ApplicationLog)

            'Protect thread locks with Try/Catch to guarantee that we let go of the lock.
            Try
                'See if anyone else is using the lock, grab it if they're not
                If Not Monitor.TryEnter(myType) Then
                    'Just wait until the other thread finishes processing, then leave if
                    '  the lock was already in use.
                    Monitor.Enter(myType)
                    Exit Sub
                End If

                'See if there is a debug configuration file specified and set up the
                '  tracing variables.
                Dim clearSettings As Boolean = True
                Try

                    'Check if we're enabled
                    If ApplicationConfiguration.TracingEnabled Then
                        'Read in the tracing switch name and create the switch.
                        Dim switchName As String = ApplicationConfiguration.TracingSwitchName
                        'Create the new switch
                        If Len(switchName) <> 0 Then
                            debugSwitch = New TraceSwitch(switchName, ApplicationConfiguration.TracingSwitchDescription)
                            debugSwitch.Level = ApplicationConfiguration.TracingTraceLevel
                        End If
                        clearSettings = False

                        'Make sure we have a TraceSettings file.
                        Dim tracingFile As String = ""
                        tracingFile = ApplicationConfiguration.TracingTraceFile()
                        If Len(tracingFile) <> 0 Then
                            'Create a jsoalog listener
                            With New FileInfo(tracingFile)
                                accessWriter = New StreamWriter(.Open(FileMode.Append, FileAccess.Write, FileShare.Read))
                            End With
                        End If

                        'Make sure we have a AuditSettings file.
                        Dim auditFile As String = ""
                        auditFile = ApplicationConfiguration.TracingAuditPZFile
                        If Len(auditFile) <> 0 Then
                            'Create a audit listener
                            With New System.IO.FileInfo(auditFile)
                                auditPZWriter = New System.IO.StreamWriter(.Open(FileMode.Append, FileAccess.Write, FileShare.Read))
                            End With
                        End If
                        auditFile = ApplicationConfiguration.TracingAuditAQFile
                        If Len(auditFile) <> 0 Then
                            'Create a audit listener
                            With New System.IO.FileInfo(auditFile)
                                auditAQWriter = New System.IO.StreamWriter(.Open(FileMode.Append, FileAccess.Write, FileShare.Read))
                            End With
                        End If
                        auditFile = ApplicationConfiguration.TracingAuditSJFile
                        If Len(auditFile) <> 0 Then
                            'Create a audit listener
                            With New System.IO.FileInfo(auditFile)
                                auditSJWriter = New System.IO.StreamWriter(.Open(FileMode.Append, FileAccess.Write, FileShare.Read))
                            End With
                        End If
                    End If
                Catch
                    'Ignore the error
                End Try

                'Use default (empty) values if something went wrong
                If clearSettings Then
                    'Tracing information is off or not properly specified, clear it
                    debugSwitch = Nothing
                    accessWriter = Nothing
                    auditPZWriter = Nothing
                    auditAQWriter = Nothing
                    auditSJWriter = Nothing
                End If

                If ApplicationConfiguration.EventLogEnabled Then
                    eventLogTraceLevel = ApplicationConfiguration.EventLogTraceLevel
                Else
                    eventLogTraceLevel = TraceLevel.Off
                End If

            Finally
                'Remove the lock from the class object
                Monitor.Exit(myType)
            End Try
        End Sub

        Protected Overrides Sub Finalize()

            MyBase.Finalize()

            Try
                If Not (auditPZWriter Is Nothing) Then
                    auditPZWriter.Close()
                    auditPZWriter = Nothing
                End If
            Catch ex As Exception
            End Try

            Try
                If Not (auditAQWriter Is Nothing) Then
                    auditAQWriter.Close()
                    auditAQWriter = Nothing
                End If
            Catch ex As Exception
            End Try

            Try
                If Not (auditSJWriter Is Nothing) Then
                    auditSJWriter.Close()
                    auditSJWriter = Nothing
                End If
            Catch ex As Exception
            End Try

            Try
                If Not (accessWriter Is Nothing) Then
                    accessWriter.Close()
                    accessWriter = Nothing
                End If
            Catch ex As Exception
            End Try

            Try
                Debug.Listeners.Clear()
            Catch ex As Exception
            End Try

        End Sub
    End Class

End Namespace
