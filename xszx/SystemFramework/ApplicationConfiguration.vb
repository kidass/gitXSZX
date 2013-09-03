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
Imports System.Configuration
Imports System.Collections
Imports System.Xml
Imports System.Collections.Specialized

Namespace Xydc.Platform.SystemFramework

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.SystemFramework
    ' 类名    ：ApplicationConfiguration
    '
    ' Description:
    '   Standard configuration settings to enable tracing and logging
    '     with the ApplicationLog class. An application can use this
    '     class as a model for adding additional settings to a
    '     Config.Web file.
    '     
    ' Special Considerations:
    '   The OnApplicationStart function in this class must be called
    '     from the Application_OnStart event in Global.asax. This is
    '     currently used only to determine the path of the application,
    '     but the HttpContext object is passed it to enable the app
    '     to read other settings in the future, and to minimize the code
    '     in global.asax. The global.asax file should be similar
    '     to the following code:
    '
    '<%@ Import Namespace="Xydc.Platform.SystemFramework" Assembly="Xydc.Platform.SystemFramework.dll" %>
    '<script language="VB" runat=server>
    '    Sub Application_OnStart()
    '        ApplicationConfiguration.OnApplicationStart Context
    '    End Sub
    '</script>
    '----------------------------------------------------------------
    Public Class ApplicationConfiguration
        Implements IConfigurationSectionHandler

        '
        ' Constant values for all of the SystemFramework standard settings
        '
        Private Const TRACING_ENABLED As String = "SystemFramework.Tracing.Enabled"
        Private Const TRACING_TRACEFILE As String = "SystemFramework.Tracing.TraceFile"
        Private Const TRACING_AUDITPZFILE As String = "SystemFramework.Tracing.AuditPZFile"
        Private Const TRACING_AUDITAQFILE As String = "SystemFramework.Tracing.AuditAQFile"
        Private Const TRACING_AUDITSJFILE As String = "SystemFramework.Tracing.AuditSJFile"
        Private Const TRACING_TRACELEVEL As String = "SystemFramework.Tracing.TraceLevel"
        Private Const TRACING_SWITCHNAME As String = "SystemFramework.Tracing.SwitchName"
        Private Const TRACING_SWITCHDESCRIPTION As String = "SystemFramework.Tracing.SwitchDescription"
        Private Const EVENTLOG_ENABLED As String = "SystemFramework.EventLog.Enabled"
        Private Const EVENTLOG_MACHINENAME As String = "SystemFramework.EventLog.Machine"
        Private Const EVENTLOG_SOURCENAME As String = "SystemFramework.EventLog.SourceName"
        Private Const EVENTLOG_TRACELEVEL As String = "SystemFramework.EventLog.LogLevel"

        '
        ' Static member variables. These contain the application settings
        '   from Config.Web, or the default values.
        '
        Private Shared fieldTracingEnabled As Boolean
        Private Shared fieldTracingTraceFile As String
        Private Shared fieldTracingAuditPZFile As String
        Private Shared fieldTracingAuditAQFile As String
        Private Shared fieldTracingAuditSJFile As String
        Private Shared fieldTracingTraceLevel As TraceLevel
        Private Shared fieldTracingSettingsFile As String
        Private Shared fieldTracingSwitchName As String
        Private Shared fieldTracingSwitchDescription As String
        Private Shared fieldEventLogEnabled As Boolean
        Private Shared fieldEventLogMachineName As String
        Private Shared fieldEventLogSourceName As String
        Private Shared fieldEventLogTraceLevel As TraceLevel

        '
        ' The root directory of the application. Established in the
        '   OnApplicationStart callback form Global.asax.
        '
        Private Shared fieldAppRoot As String

        '
        ' Constant values for all of the default settings.
        '
        Private Const TRACING_ENABLED_DEFAULT As Boolean = False
        Private Const TRACING_TRACEFILE_DEFAULT As String = "jsoa.log"
        Private Const TRACING_AUDITPZFILE_DEFAULT As String = "auditPZ.log"
        Private Const TRACING_AUDITAQFILE_DEFAULT As String = "auditAQ.log"
        Private Const TRACING_AUDITSJFILE_DEFAULT As String = "auditSJ.log"
        Private Const TRACING_TRACELEVEL_DEFAULT As TraceLevel = TraceLevel.Verbose
        Private Const TRACING_SWITCHNAME_DEFAULT As String = "ApplicationTraceSwitch"
        Private Const TRACING_SWITCHDESCRIPTION_DEFAULT As String = "Application error and tracing information"
        Private Const EVENTLOG_ENABLED_DEFAULT As Boolean = True
        Private Const EVENTLOG_MACHINENAME_DEFAULT As String = "."
        Private Const EVENTLOG_SOURCENAME_DEFAULT As String = "WebApplication"
        Private Const EVENTLOG_TRACELEVEL_DEFAULT As TraceLevel = TraceLevel.Error

        '----------------------------------------------------------------
        ' Function Create:
        '   Called by ASP+ before the application starts to initialize
        '     settings from the Config.Web file(s). The app domain will
        '     restart if these settings change, so there is no reason
        '     to read these values more than once. This function uses the
        '     DictionarySectionHandler base class to generate a hashtable
        '     from the XML, which is then used to store the current settings.
        '     Because all settings are read here, we do not actually store
        '     the generated hashtable object for later retrieval by
        '     Context.GetConfig. The application should use the accessor
        '     functions directly.
        ' Returns:
        '   A ConfigOutput object, which we leave empty because all settings
        '     are stored at this point.
        ' Parameters:
        '   [in] parent: An object created by processing a section with this name
        '                in a Config.Web file in a parent directory.
        '   [in] configContext: The config's context.
        '   [in] section: The section to be read.
        '----------------------------------------------------------------
        Function Create(ByVal parent As Object, ByVal configContext As Object, ByVal section As System.Xml.XmlNode) As Object Implements IConfigurationSectionHandler.Create

            Dim settings As System.Collections.Specialized.NameValueCollection

            Try
                Dim baseHandler As NameValueSectionHandler
                baseHandler = New NameValueSectionHandler
                settings = CType(baseHandler.Create(parent, configContext, section), System.Collections.Specialized.NameValueCollection)
            Catch
            End Try

            If settings Is Nothing Then
                fieldTracingEnabled = TRACING_ENABLED_DEFAULT
                fieldTracingTraceFile = TRACING_TRACEFILE_DEFAULT
                fieldTracingAuditPZFile = TRACING_AUDITPZFILE_DEFAULT
                fieldTracingAuditAQFile = TRACING_AUDITAQFILE_DEFAULT
                fieldTracingAuditSJFile = TRACING_AUDITSJFILE_DEFAULT
                fieldTracingTraceLevel = TRACING_TRACELEVEL_DEFAULT
                fieldTracingSwitchName = TRACING_SWITCHNAME_DEFAULT
                fieldTracingSwitchDescription = TRACING_SWITCHDESCRIPTION_DEFAULT
                fieldEventLogEnabled = EVENTLOG_ENABLED_DEFAULT
                fieldEventLogMachineName = EVENTLOG_MACHINENAME_DEFAULT
                fieldEventLogSourceName = EVENTLOG_SOURCENAME_DEFAULT
                fieldEventLogTraceLevel = EVENTLOG_TRACELEVEL_DEFAULT
                Exit Function
            Else
                fieldTracingEnabled = ReadSetting(settings, TRACING_ENABLED, TRACING_ENABLED_DEFAULT)
                fieldTracingTraceFile = ReadSetting(settings, TRACING_TRACEFILE, TRACING_TRACEFILE_DEFAULT)
                fieldTracingAuditPZFile = ReadSetting(settings, TRACING_AUDITPZFILE, TRACING_AUDITPZFILE_DEFAULT)
                fieldTracingAuditAQFile = ReadSetting(settings, TRACING_AUDITAQFILE, TRACING_AUDITAQFILE_DEFAULT)
                fieldTracingAuditSJFile = ReadSetting(settings, TRACING_AUDITSJFILE, TRACING_AUDITSJFILE_DEFAULT)
                fieldTracingTraceLevel = ReadSetting(settings, TRACING_TRACELEVEL, TRACING_TRACELEVEL_DEFAULT)
                fieldTracingSwitchName = ReadSetting(settings, TRACING_SWITCHNAME, TRACING_SWITCHNAME_DEFAULT)
                fieldTracingSwitchDescription = ReadSetting(settings, TRACING_SWITCHDESCRIPTION, TRACING_SWITCHDESCRIPTION_DEFAULT)
                fieldEventLogEnabled = ReadSetting(settings, EVENTLOG_ENABLED, EVENTLOG_ENABLED_DEFAULT)
                fieldEventLogMachineName = ReadSetting(settings, EVENTLOG_MACHINENAME, EVENTLOG_MACHINENAME_DEFAULT)
                fieldEventLogSourceName = ReadSetting(settings, EVENTLOG_SOURCENAME, EVENTLOG_SOURCENAME_DEFAULT)
                fieldEventLogTraceLevel = ReadSetting(settings, EVENTLOG_TRACELEVEL, EVENTLOG_TRACELEVEL_DEFAULT)
            End If

        End Function

        '----------------------------------------------------------------
        ' Shared Function ReadSetting:
        '   Reads a setting from a hashtable and converts it to the correct
        '     type. One of these functions is provided for each type
        '     expected in the hash table. These are public so that other
        '     classes don't have to duplicate them to read settings from
        '     a hash table.
        ' Returns:
        '   The value from the hash table, or the default if the item is not
        '     in the table or cannot be case to the expected type.
        ' Parameters:
        '   [in] settings: The Hashtable to read from
        '   [in] key: A key for the value in the Hashtable
        '   [in] default: The default value if the item is not found.
        '----------------------------------------------------------------

        '----------------------------------------------------------------
        ' String version of ReadSetting
        '----------------------------------------------------------------
        Public Overloads Shared Function ReadSetting(ByVal settings As NameValueCollection, ByVal key As String, ByVal defaultValue As String) As String

            Try
                Dim setting As Object = settings(key)
                If setting Is Nothing Then
                    ReadSetting = defaultValue
                Else
                    ReadSetting = CStr(setting)
                End If
            Catch
                ReadSetting = defaultValue
            End Try

        End Function

        '----------------------------------------------------------------
        ' Boolean version of ReadSetting
        '----------------------------------------------------------------
        Public Overloads Shared Function ReadSetting(ByVal settings As NameValueCollection, ByVal key As String, ByVal defaultValue As Boolean) As Boolean

            Try
                Dim setting As Object = settings(key)
                If setting Is Nothing Then
                    ReadSetting = defaultValue
                Else
                    ReadSetting = CBool(setting)
                End If
            Catch
                ReadSetting = defaultValue
            End Try

        End Function

        '----------------------------------------------------------------
        ' Long version of ReadSetting
        '----------------------------------------------------------------
        Public Overloads Shared Function ReadSetting(ByVal settings As NameValueCollection, ByVal key As String, ByVal defaultValue As Integer) As Integer

            Try
                Dim setting As Object = settings(key)
                If setting Is Nothing Then
                    ReadSetting = defaultValue
                Else
                    ReadSetting = CInt(setting)
                End If
            Catch
                ReadSetting = defaultValue
            End Try

        End Function

        '----------------------------------------------------------------
        ' TraceLevel version of ReadSetting
        '----------------------------------------------------------------
        Public Overloads Shared Function ReadSetting(ByVal settings As NameValueCollection, ByVal key As String, ByVal defaultValue As TraceLevel) As TraceLevel

            Try
                Dim setting As Object = settings(key)
                If setting Is Nothing Then
                    ReadSetting = defaultValue
                Else
                    ReadSetting = CType(CInt(setting), TraceLevel)
                End If
            Catch
                ReadSetting = defaultValue
            End Try

        End Function

        '----------------------------------------------------------------
        ' Shared Sub OnApplicationStart:
        '   Function to be called by Application_OnStart as described in the
        '     class description. Initializes the application root.
        ' Parameters:
        '   [in] AppRoot: The path of the running application.
        '----------------------------------------------------------------
        Public Shared Sub OnApplicationStart(ByVal AppRoot As String)

            fieldAppRoot = AppRoot
            System.Configuration.ConfigurationManager.GetSection("ApplicationConfiguration1")
            System.Configuration.ConfigurationManager.GetSection("jsoaConfiguration1")

            'System.Configuration.ConfigurationSettings.GetConfig("ApplicationConfiguration")
            'System.Configuration.ConfigurationSettings.GetConfig("jsoaConfiguration")

        End Sub

        '----------------------------------------------------------------
        ' Shared Property Get AppRoot:
        '   Retrieve the root path of the application
        ' Returns:
        '   Path
        '----------------------------------------------------------------
        Public Shared ReadOnly Property AppRoot() As String

            Get
                AppRoot = fieldAppRoot
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get TracingEnabled:
        '   Retrieve the configuration setting, defaulting to False on error
        ' Returns:
        '   True if the trace file should be used
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TracingEnabled() As Boolean

            Get
                TracingEnabled = fieldTracingEnabled
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get TracingTraceFile:
        '   Retrieve the file that contains trace settings
        ' Returns:
        '   A full path name
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TracingTraceFile() As String

            Get
                TracingTraceFile = fieldTracingTraceFile
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get TracingAuditPZFile:
        '   Retrieve the file that contains trace settings
        ' Returns:
        '   A full path name
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TracingAuditPZFile() As String

            Get
                TracingAuditPZFile = fieldTracingAuditPZFile
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get TracingAuditAQFile:
        '   Retrieve the file that contains trace settings
        ' Returns:
        '   A full path name
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TracingAuditAQFile() As String

            Get
                TracingAuditAQFile = fieldTracingAuditAQFile
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get TracingAuditSJFile:
        '   Retrieve the file that contains trace settings
        ' Returns:
        '   A full path name
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TracingAuditSJFile() As String

            Get
                TracingAuditSJFile = fieldTracingAuditSJFile
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get TracingTraceLevel:
        '   The highest logging level that should be written to the tracing file,
        '     defaults to TraceLevel.Verbose (however, TracingEnabled defaults
        '     to False, so you have to turn it on explicitly).
        ' Returns:
        '   TraceLevel
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TracingTraceLevel() As TraceLevel

            Get
                TracingTraceLevel = fieldTracingTraceLevel
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get TracingSwitchName:
        '   Retrieve the trace switch name, defaults to ApplicationTraceSwitch
        ' Returns:
        '   The switch name
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TracingSwitchName() As String

            Get
                TracingSwitchName = fieldTracingSwitchName
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get TracingSwitchDescription:
        '   Retrieve the trace settings file, defaults to "Application error and
        '     tracing information"
        ' Returns:
        '   The switch description
        '----------------------------------------------------------------
        Public Shared ReadOnly Property TracingSwitchDescription() As String

            Get
                TracingSwitchDescription = fieldTracingSwitchDescription
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get EventLogEnabled:
        '   Retrieve whether writing to the event log is support, defaults to True
        ' Returns:
        '   True if writing to the event log is enabled
        '----------------------------------------------------------------
        Public Shared ReadOnly Property EventLogEnabled() As Boolean

            Get
                EventLogEnabled = fieldEventLogEnabled
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get EventLogMachineName:
        '   Retrieve the machine name to log the event to, defaults to an
        '     empty string, indicating the current machine.
        ' Returns:
        '   A machine name (without \\), may be empty
        '----------------------------------------------------------------
        Public Shared ReadOnly Property EventLogMachineName() As String

            Get
                EventLogMachineName = fieldEventLogMachineName
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get EventLogSourceName:
        '   The source of the error to be written to the event log, defaults
        '     WebApplication.
        ' Returns:
        '   String
        '----------------------------------------------------------------
        Public Shared ReadOnly Property EventLogSourceName() As String

            Get
                EventLogSourceName = fieldEventLogSourceName
            End Get

        End Property

        '----------------------------------------------------------------
        ' Shared Property Get EventLogTraceLevel:
        '   The highest logging level that should be written to the event log,
        '     defaults to TraceLevel.Error.
        ' Returns:
        '   TraceLevel
        '----------------------------------------------------------------
        Public Shared ReadOnly Property EventLogTraceLevel() As TraceLevel

            Get
                EventLogTraceLevel = fieldEventLogTraceLevel
            End Get

        End Property

    End Class

End Namespace
