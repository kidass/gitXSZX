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
Imports System.Diagnostics
Imports System.Text

Imports Microsoft.VisualBasic

Namespace Xydc.Platform.SystemFramework

    '----------------------------------------------------------------
    ' 命名空间： Xydc.Platform.SystemFramework
    ' 类名    ：ApplicationAssert
    '
    ' 功能描述： 
    '   A class to help with error checking and automatic logging
    '     of asserts and conditional checks. This class works with 
    '     displays system assert dialogs as well as writing to the
    '     application log with the ApplicationLog class. There is
    '     no instance data associated with this class.
    '----------------------------------------------------------------
    Public Class ApplicationAssert

#If Debug = 0 Then
        'A LineNumber constant to be used when not in a debug build
        '  so that ApplicationAssert.LineNumber is always a valid expression.
        '  This allows us to pass ApplicationAssert.LineNumber with good debug
        '  functionality and minimal runtime overhead.
        Public Const LineNumber As Integer = 0
#Else
        '----------------------------------------------------------------
        ' Shared Property Get LineNumber:
        '   Get the current line number in the calling function. This should
        '     be called in a parameter list to get accurate information about
        '     the line number before the Check* functions are called. If we wait
        '     until the Check* functions themselves to retrieve this information,
        '     then the stack trace indicates the next executable line, which is
        '     only marginally useful information. This function is compiled out in
        '     debug builds in favor of the LineNumber constant.
        ' Returns:
        '   LineNumber, or 0 on failure.
        ' Parameters:
        '   [in] LineNumber: The line of the current error in the function. This
        '                    value should be retrieved by call Application.LineNumber
        '                    in the parameter list of any of the Check* functions. If
        '                    LineNumber is not provided,then the next executable line is used.
        '----------------------------------------------------------------
        Public Shared ReadOnly Property LineNumber() As Integer
            Get
                Try
                    '
                    ' Get the trace information with file information by skipping
                    '   this function and then reading the top stack frame.
                    '
                    With New StackTrace(1, True)
                        LineNumber = .GetFrame(0).GetFileLineNumber
                    End With
                Catch
                End Try
            End Get
        End Property
#End If

        '----------------------------------------------------------------
        ' Shared Sub Check:
        '   Check the given condition and show an assert dialog when the
        '     desktop is interactive. Log the assertion at a warning level in
        '     case the desktop is not interactive. The text will always 
        '     contain full stack trace information and will show the location
        '     of the error condition if the source code is available.
        ' Parameters:
        '   [in] condition: An expression to be tested for True
        '   [in] errorText: The message to display
        '   [in] lineNumber: The line of the current error in the function. See
        '                    GenerateStackTrace for more information.
        '----------------------------------------------------------------
        Public Shared Sub Check(ByVal condition As Boolean, ByVal errorText As String, Optional ByVal lineNumber As Integer = 0)
            'Only check the condition for debug builds
#If Debug = 1 Then
                Dim detailMessage As String
                Dim strBuilder As StringBuilder

                If Not condition Then
                    detailMessage = GenerateStackTrace(lineNumber)
                    strBuilder = New StringBuilder
                    strBuilder.Append("Assert: ").Append(ControlChars.CrLf).Append(errorText).Append(ControlChars.CrLf).Append(detailMessage)
                    ApplicationLog.WriteWarning(strBuilder.ToString)
                    Debug.Fail(errorText, detailMessage)
                End If
#End If
        End Sub

        '----------------------------------------------------------------
        ' Shared Sub CheckCondition:
        '   Verify that a required condition holds. Show an assert dialog in
        '     a DEBUG build before throwing an ApplicationException.
        '     It is assumed that the exception will be handled or logged, so this
        '     does not log a warning for the assertion like the Check function, which
        '     does not actually throw.
        ' Parameters:
        '   [in] condition: An expression to be tested for True
        '   [in] errorText: The message to display
        '   [in] lineNumber: The line of the current error in the function. See
        '                    GenerateStackTrace for more information.
        '----------------------------------------------------------------
        Public Shared Sub CheckCondition(ByVal condition As Boolean, ByVal errorText As String, Optional ByVal lineNumber As Integer = 0)

            'Test the condition
            If Not condition Then

                'Assert and throw if the condition is not met
#If Debug = 1 Then
                    Debug.Fail(errorText, GenerateStackTrace(lineNumber))
#End If

                Throw (New ApplicationException(errorText))
            End If
        End Sub

#If Debug = 1 Then
        '----------------------------------------------------------------
        ' Shared Function GenerateStackTrace:
        '   Generate a stack trace to display/log with the assertion text.
        '     The trace information includes file and line number information
        '     if its available, as well as a copy of the line of text if
        '     the source code is available. This function is only included in
        '     DEBUG builds of the application.
        ' Parameters:
        '   [in] lineNumber: The line of the current error in the function. This
        '                    value should be retrieved by call Application.LineNumber
        '                    in the parameter list of any of the Check* functions. If
        '                    lineNumber is not provided,then the next executable line is used.
        '----------------------------------------------------------------
        Private Shared Function GenerateStackTrace(ByVal lineNumber As Integer) As String
            Dim message As StringBuilder 'Used for smart string concatenation
            Dim fileName As String       'The source file name
            Dim currentLine As Integer   'The line to process in the source file
            Dim sourceLine As String     'The line from the source file
            Dim fileHandle As Integer    'The file number used for reading the source code
            Dim openedFile As Boolean

            message = New StringBuilder

            'New StackTrace should never fail, but Try/Catch to be rock solid.
            Try

                'Get a new stack trace with line information. Skip the first function
                ' and second functions (this one, and the calling Check* function)
                With New StackTrace(2, True)
                    Try

                        '
                        ' Get the first retrieved stack frame and attempt to get
                        '   file information from the trace, then open the file
                        '   and find the specified line. Display as much information
                        '   as possible if this is not supported.
                        '
                        With .GetFrame(0)

                            'Retrieve and add File/Line information
                            fileName = .GetFileName

                            ' File Name may not be available
                            If fileName Is Nothing Then fileName = "<UnknownName>"

                            If lineNumber <> 0 Then
                                currentLine = lineNumber
                            Else
                                currentLine = .GetFileLineNumber
                            End If

                            If fileName <> "<UnknownName>" And currentLine >= 0 Then
                                'Append File name and line number
                                message.Append(fileName).Append(", Line: ").Append(currentLine)

                                'Append the actual code if we can find the source file
                                fileHandle = FreeFile
                                FileOpen(fileHandle, fileName, OpenMode.Input)
                                openedFile = True

                                Do
                                    sourceLine = LineInput(fileHandle)
                                    currentLine = currentLine - 1
                                Loop While currentLine <> 0

                                message.Append(ControlChars.CrLf)

                                If lineNumber <> 0 Then
                                    message.Append("Current executable line:")
                                Else
                                    message.Append(ControlChars.CrLf).Append("Next executable line:")
                                End If

                                message.Append(ControlChars.CrLf).Append(sourceLine.Trim())
                            End If
                        End With
                    Catch
                        'Ignore errors, just show as much as we can
                    Finally
                        'Always close the file
                        If openedFile Then FileClose(fileHandle)
                    End Try

                    'Retrieve the final string
                    GenerateStackTrace = message.ToString
                End With
            Catch
                'Nothing to do, just get out of here with the default (empty) return value
            End Try
        End Function
#End If

    End Class

End Namespace
