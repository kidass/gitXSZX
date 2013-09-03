Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IMFlowEditWord
    '
    ' 功能描述： 
    '     flow_editword.aspx模块本身恢复现场需要的信息
    '----------------------------------------------------------------
    <Serializable()> Public Class IMFlowEditWord
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtProtectPassword As String        'htxtProtectPassword
        Private m_strhtxtUserName As String               'htxtUserName
        Private m_strhtxtTrackRevisions As String         'htxtTrackRevisions
        Private m_strhtxtEditMode As String               'htxtEditMode
        '2009-02-20
        Private m_strhtxtLocked As String               'htxtLocked
        '2009-02-20
        Private m_strhtxtFileSpec As String               'htxtFileSpec
        Private m_strhtxtAutoSave As String               'htxtAutoSave
        Private m_strhtxtCanQSYJ As String                'htxtCanQSYJ
        Private m_strhtxtCanImportFile As String          'htxtCanImportFile
        Private m_strhtxtCanExportFile As String          'htxtCanExportFile
        Private m_strhtxtCanSelectTGWJ As String          'htxtCanSelectTGWJ
        Private m_strhtxtCursorPos As String              'htxtCursorPos
        Private m_strhtxtDivLeftBody As String            'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String             'htxtDivTopBody














        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strhtxtProtectPassword = ""
            m_strhtxtUserName = ""
            m_strhtxtTrackRevisions = "0"
            m_strhtxtEditMode = "0"
            '2009-02-20
            m_strhtxtLocked = "0"
            '2009-02-20
            m_strhtxtFileSpec = ""
            m_strhtxtAutoSave = "0"
            m_strhtxtCanQSYJ = "0"
            m_strhtxtCanImportFile = "0"
            m_strhtxtCanExportFile = "0"
            m_strhtxtCanSelectTGWJ = "0"
            m_strhtxtCursorPos = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMFlowEditWord)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' htxtTrackRevisions属性
        '----------------------------------------------------------------
        Public Property htxtTrackRevisions() As String
            Get
                htxtTrackRevisions = m_strhtxtTrackRevisions
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtTrackRevisions = Value
                Catch ex As Exception
                    m_strhtxtTrackRevisions = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtUserName属性
        '----------------------------------------------------------------
        Public Property htxtUserName() As String
            Get
                htxtUserName = m_strhtxtUserName
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtUserName = Value
                Catch ex As Exception
                    m_strhtxtUserName = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtEditMode属性
        '----------------------------------------------------------------
        Public Property htxtEditMode() As String
            Get
                htxtEditMode = m_strhtxtEditMode
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtEditMode = Value
                Catch ex As Exception
                    m_strhtxtEditMode = ""
                End Try
            End Set
        End Property

        '2009-02-20
        '----------------------------------------------------------------
        ' htxtLocked属性
        '----------------------------------------------------------------
        Public Property htxtLocked() As String
            Get
                htxtLocked = m_strhtxtLocked
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLocked = Value
                Catch ex As Exception
                    m_strhtxtLocked = ""
                End Try
            End Set
        End Property
        '2009-02-20



        '----------------------------------------------------------------
        ' htxtProtectPassword属性
        '----------------------------------------------------------------
        Public Property htxtProtectPassword() As String
            Get
                htxtProtectPassword = m_strhtxtProtectPassword
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtProtectPassword = Value
                Catch ex As Exception
                    m_strhtxtProtectPassword = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFileSpec属性
        '----------------------------------------------------------------
        Public Property htxtFileSpec() As String
            Get
                htxtFileSpec = m_strhtxtFileSpec
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFileSpec = Value
                Catch ex As Exception
                    m_strhtxtFileSpec = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtAutoSave属性
        '----------------------------------------------------------------
        Public Property htxtAutoSave() As String
            Get
                htxtAutoSave = m_strhtxtAutoSave
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtAutoSave = Value
                Catch ex As Exception
                    m_strhtxtAutoSave = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCanQSYJ属性
        '----------------------------------------------------------------
        Public Property htxtCanQSYJ() As String
            Get
                htxtCanQSYJ = m_strhtxtCanQSYJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCanQSYJ = Value
                Catch ex As Exception
                    m_strhtxtCanQSYJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCanImportFile属性
        '----------------------------------------------------------------
        Public Property htxtCanImportFile() As String
            Get
                htxtCanImportFile = m_strhtxtCanImportFile
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCanImportFile = Value
                Catch ex As Exception
                    m_strhtxtCanImportFile = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCanExportFile属性
        '----------------------------------------------------------------
        Public Property htxtCanExportFile() As String
            Get
                htxtCanExportFile = m_strhtxtCanExportFile
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCanExportFile = Value
                Catch ex As Exception
                    m_strhtxtCanExportFile = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCanSelectTGWJ属性
        '----------------------------------------------------------------
        Public Property htxtCanSelectTGWJ() As String
            Get
                htxtCanSelectTGWJ = m_strhtxtCanSelectTGWJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCanSelectTGWJ = Value
                Catch ex As Exception
                    m_strhtxtCanSelectTGWJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtCursorPos属性
        '----------------------------------------------------------------
        Public Property htxtCursorPos() As String
            Get
                htxtCursorPos = m_strhtxtCursorPos
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtCursorPos = Value
                Catch ex As Exception
                    m_strhtxtCursorPos = ""
                End Try
            End Set
        End Property








        '----------------------------------------------------------------
        ' htxtDivLeftBody属性
        '----------------------------------------------------------------
        Public Property htxtDivLeftBody() As String
            Get
                htxtDivLeftBody = m_strhtxtDivLeftBody
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftBody = Value
                Catch ex As Exception
                    m_strhtxtDivLeftBody = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopBody属性
        '----------------------------------------------------------------
        Public Property htxtDivTopBody() As String
            Get
                htxtDivTopBody = m_strhtxtDivTopBody
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopBody = Value
                Catch ex As Exception
                    m_strhtxtDivTopBody = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
