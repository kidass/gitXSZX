Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMChatFsxx
    '
    ' ���������� 
    '     chat_fsxx.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMChatFsxx
        Implements IDisposable

        '----------------------------------------------------------------
        'textbox
        '----------------------------------------------------------------
        Private m_strtextareaNR As String                             'textareaNR

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtSessionIdFJ As String                        'htxtSessionIdFJ
        Private m_strhtxtReplyMode As String                          'htxtReplyMode
        Private m_strhtxtLSH As String                                'htxtLSH
        Private m_strtxtJSR As String                                 'txtJSR

        Private m_strhtxtFJQuery As String                            'htxtFJQuery
        Private m_strhtxtFJRows As String                             'htxtFJRows
        Private m_strhtxtFJSort As String                             'htxtFJSort
        Private m_strhtxtFJSortColumnIndex As String                  'htxtFJSortColumnIndex
        Private m_strhtxtFJSortType As String                         'htxtFJSortType
        Private m_strhtxtDivLeftFJ As String                          'htxtDivLeftFJ
        Private m_strhtxtDivTopFJ As String                           'htxtDivTopFJ
        Private m_strhtxtDivLeftBody As String                        'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                         'htxtDivTopBody

        '----------------------------------------------------------------
        'grdFJ paramters
        '----------------------------------------------------------------
        Private m_intPageSize_grdFJ As Integer                        'grdFJ��ҳ��С
        Private m_intSelectedIndex_grdFJ As Integer                   'grdFJ��������
        Private m_intCurrentPageIndex_grdFJ As Integer                'grdFJ��ҳ����











        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()

            MyBase.New()

            m_strtextareaNR = ""

            m_strhtxtSessionIdFJ = ""
            m_strhtxtReplyMode = ""
            m_strhtxtLSH = ""
            m_strtxtJSR = ""

            m_strhtxtFJQuery = ""
            m_strhtxtFJRows = ""
            m_strhtxtFJSort = ""
            m_strhtxtFJSortColumnIndex = ""
            m_strhtxtFJSortType = ""

            m_strhtxtDivLeftFJ = ""
            m_strhtxtDivTopFJ = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_intPageSize_grdFJ = 100
            m_intSelectedIndex_grdFJ = -1
            m_intCurrentPageIndex_grdFJ = 0

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMChatFsxx)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub















        '----------------------------------------------------------------
        ' textareaNR����
        '----------------------------------------------------------------
        Public Property textareaNR() As String
            Get
                textareaNR = m_strtextareaNR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtextareaNR = Value
                Catch ex As Exception
                    m_strtextareaNR = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtSessionIdFJ����
        '----------------------------------------------------------------
        Public Property htxtSessionIdFJ() As String
            Get
                htxtSessionIdFJ = m_strhtxtSessionIdFJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdFJ = Value
                Catch ex As Exception
                    m_strhtxtSessionIdFJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtReplyMode����
        '----------------------------------------------------------------
        Public Property htxtReplyMode() As String
            Get
                htxtReplyMode = m_strhtxtReplyMode
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtReplyMode = Value
                Catch ex As Exception
                    m_strhtxtReplyMode = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLSH����
        '----------------------------------------------------------------
        Public Property htxtLSH() As String
            Get
                htxtLSH = m_strhtxtLSH
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLSH = Value
                Catch ex As Exception
                    m_strhtxtLSH = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtJSR����
        '----------------------------------------------------------------
        Public Property txtJSR() As String
            Get
                txtJSR = m_strtxtJSR
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtJSR = Value
                Catch ex As Exception
                    m_strtxtJSR = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtFJSort����
        '----------------------------------------------------------------
        Public Property htxtFJSort() As String
            Get
                htxtFJSort = m_strhtxtFJSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSort = Value
                Catch ex As Exception
                    m_strhtxtFJSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJRows����
        '----------------------------------------------------------------
        Public Property htxtFJRows() As String
            Get
                htxtFJRows = m_strhtxtFJRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJRows = Value
                Catch ex As Exception
                    m_strhtxtFJRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtFJSortColumnIndex() As String
            Get
                htxtFJSortColumnIndex = m_strhtxtFJSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtFJSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJQuery����
        '----------------------------------------------------------------
        Public Property htxtFJQuery() As String
            Get
                htxtFJQuery = m_strhtxtFJQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJQuery = Value
                Catch ex As Exception
                    m_strhtxtFJQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtFJSortType����
        '----------------------------------------------------------------
        Public Property htxtFJSortType() As String
            Get
                htxtFJSortType = m_strhtxtFJSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtFJSortType = Value
                Catch ex As Exception
                    m_strhtxtFJSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftFJ����
        '----------------------------------------------------------------
        Public Property htxtDivLeftFJ() As String
            Get
                htxtDivLeftFJ = m_strhtxtDivLeftFJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftFJ = Value
                Catch ex As Exception
                    m_strhtxtDivLeftFJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopFJ����
        '----------------------------------------------------------------
        Public Property htxtDivTopFJ() As String
            Get
                htxtDivTopFJ = m_strhtxtDivTopFJ
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopFJ = Value
                Catch ex As Exception
                    m_strhtxtDivTopFJ = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivLeftBody����
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
        ' htxtDivTopBody����
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




        '----------------------------------------------------------------
        ' grdFJ_PageSize����
        '----------------------------------------------------------------
        Public Property grdFJ_PageSize() As Integer
            Get
                grdFJ_PageSize = m_intPageSize_grdFJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdFJ = Value
                Catch ex As Exception
                    m_intPageSize_grdFJ = 100
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFJ_SelectedIndex����
        '----------------------------------------------------------------
        Public Property grdFJ_SelectedIndex() As Integer
            Get
                grdFJ_SelectedIndex = m_intSelectedIndex_grdFJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdFJ = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdFJ = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdFJ_CurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdFJ_CurrentPageIndex() As Integer
            Get
                grdFJ_CurrentPageIndex = m_intCurrentPageIndex_grdFJ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdFJ = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdFJ = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
