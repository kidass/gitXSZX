Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IMGgxxLdap
    '
    ' ���������� 
    '     ggxx_ldap.aspxģ�鱾��ָ��ֳ���Ҫ����Ϣ
    '----------------------------------------------------------------
    <Serializable()> Public Class IMGgxxLdap
        Implements IDisposable

        '----------------------------------------------------------------
        'hidden textbox
        '----------------------------------------------------------------
        Private m_strhtxtLDAPQuery As String                      'htxtLDAPQuery
        Private m_strhtxtLDAPRows As String                       'htxtLDAPRows
        Private m_strhtxtLDAPSort As String                       'htxtLDAPSort
        Private m_strhtxtLDAPSortColumnIndex As String            'htxtLDAPSortColumnIndex
        Private m_strhtxtLDAPSortType As String                   'htxtLDAPSortType
        Private m_strhtxtDivLeftLDAP As String                    'htxtDivLeftLDAP
        Private m_strhtxtDivTopLDAP As String                     'htxtDivTopLDAP
        Private m_strhtxtDivLeftBody As String                    'htxtDivLeftBody
        Private m_strhtxtDivTopBody As String                     'htxtDivTopBody

        Private m_strhtxtSessionIdQueryLDAP As String             'htxtSessionIdQueryLDAP

        '----------------------------------------------------------------
        'asp:textbox
        '----------------------------------------------------------------
        Private m_strtxtLDAPPageIndex As String                  'txtLDAPPageIndex
        Private m_strtxtLDAPPageSize As String                   'txtLDAPPageSize
        Private m_strtxtNF As String                             'txtNF

        Private m_strtxtLD As String                             'txtLD
        Private m_strtxtDD As String                             'txtDD
        Private m_strtxtHD As String                             'txtHD

        Private m_intSelectedIndex_ddlYF As Integer              'ddlYF
        Private m_strtxtLDAPSearch_APRQMin As String             'txtLDAPSearch_APRQMin
        Private m_strtxtLDAPSearch_APRQMax As String             'txtLDAPSearch_APRQMax

        '----------------------------------------------------------------
        'asp:datagrid - grdLDAP
        '----------------------------------------------------------------
        Private m_intPageSize_grdLDAP As Integer
        Private m_intSelectedIndex_grdLDAP As Integer
        Private m_intCurrentPageIndex_grdLDAP As Integer

        '----------------------------------------------------------------
        'asp:RadioButtonList - rblLDAPSearchAPRQ
        '----------------------------------------------------------------
        Private m_intSelectedIndex_rblLDAPSearchAPRQ As Integer












        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            'hidden
            m_strhtxtLDAPQuery = ""
            m_strhtxtLDAPRows = ""
            m_strhtxtLDAPSort = ""
            m_strhtxtLDAPSortColumnIndex = ""
            m_strhtxtLDAPSortType = ""
            m_strhtxtDivLeftLDAP = ""
            m_strhtxtDivTopLDAP = ""
            m_strhtxtDivLeftBody = ""
            m_strhtxtDivTopBody = ""

            m_strhtxtSessionIdQueryLDAP = ""

            'textbox
            m_strtxtLDAPPageIndex = ""
            m_strtxtLDAPPageSize = ""
            m_strtxtNF = ""

            m_strtxtLD = ""
            m_strtxtDD = ""
            m_strtxtHD = ""

            m_intSelectedIndex_ddlYF = -1
            m_strtxtLDAPSearch_APRQMin = ""
            m_strtxtLDAPSearch_APRQMax = ""

            'datagrid
            m_intPageSize_grdLDAP = 0
            m_intCurrentPageIndex_grdLDAP = 0
            m_intSelectedIndex_grdLDAP = -1

            'RadioButtonList
            m_intSelectedIndex_rblLDAPSearchAPRQ = -1

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
        Public Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IMGgxxLdap)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub














        '----------------------------------------------------------------
        ' htxtLDAPQuery����
        '----------------------------------------------------------------
        Public Property htxtLDAPQuery() As String
            Get
                htxtLDAPQuery = m_strhtxtLDAPQuery
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPQuery = Value
                Catch ex As Exception
                    m_strhtxtLDAPQuery = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLDAPRows����
        '----------------------------------------------------------------
        Public Property htxtLDAPRows() As String
            Get
                htxtLDAPRows = m_strhtxtLDAPRows
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPRows = Value
                Catch ex As Exception
                    m_strhtxtLDAPRows = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLDAPSort����
        '----------------------------------------------------------------
        Public Property htxtLDAPSort() As String
            Get
                htxtLDAPSort = m_strhtxtLDAPSort
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPSort = Value
                Catch ex As Exception
                    m_strhtxtLDAPSort = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLDAPSortColumnIndex����
        '----------------------------------------------------------------
        Public Property htxtLDAPSortColumnIndex() As String
            Get
                htxtLDAPSortColumnIndex = m_strhtxtLDAPSortColumnIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPSortColumnIndex = Value
                Catch ex As Exception
                    m_strhtxtLDAPSortColumnIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtLDAPSortType����
        '----------------------------------------------------------------
        Public Property htxtLDAPSortType() As String
            Get
                htxtLDAPSortType = m_strhtxtLDAPSortType
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtLDAPSortType = Value
                Catch ex As Exception
                    m_strhtxtLDAPSortType = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' htxtDivLeftLDAP����
        '----------------------------------------------------------------
        Public Property htxtDivLeftLDAP() As String
            Get
                htxtDivLeftLDAP = m_strhtxtDivLeftLDAP
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivLeftLDAP = Value
                Catch ex As Exception
                    m_strhtxtDivLeftLDAP = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' htxtDivTopLDAP����
        '----------------------------------------------------------------
        Public Property htxtDivTopLDAP() As String
            Get
                htxtDivTopLDAP = m_strhtxtDivTopLDAP
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtDivTopLDAP = Value
                Catch ex As Exception
                    m_strhtxtDivTopLDAP = ""
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
        ' htxtSessionIdQueryLDAP����
        '----------------------------------------------------------------
        Public Property htxtSessionIdQueryLDAP() As String
            Get
                htxtSessionIdQueryLDAP = m_strhtxtSessionIdQueryLDAP
            End Get
            Set(ByVal Value As String)
                Try
                    m_strhtxtSessionIdQueryLDAP = Value
                Catch ex As Exception
                    m_strhtxtSessionIdQueryLDAP = ""
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' txtLDAPPageIndex����
        '----------------------------------------------------------------
        Public Property txtLDAPPageIndex() As String
            Get
                txtLDAPPageIndex = m_strtxtLDAPPageIndex
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLDAPPageIndex = Value
                Catch ex As Exception
                    m_strtxtLDAPPageIndex = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLDAPPageSize����
        '----------------------------------------------------------------
        Public Property txtLDAPPageSize() As String
            Get
                txtLDAPPageSize = m_strtxtLDAPPageSize
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLDAPPageSize = Value
                Catch ex As Exception
                    m_strtxtLDAPPageSize = ""
                End Try
            End Set
        End Property








        '----------------------------------------------------------------
        ' txtLD����
        '----------------------------------------------------------------
        Public Property txtLD() As String
            Get
                txtLD = m_strtxtLD
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLD = Value
                Catch ex As Exception
                    m_strtxtLD = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtDD����
        '----------------------------------------------------------------
        Public Property txtDD() As String
            Get
                txtDD = m_strtxtDD
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtDD = Value
                Catch ex As Exception
                    m_strtxtDD = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtHD����
        '----------------------------------------------------------------
        Public Property txtHD() As String
            Get
                txtHD = m_strtxtHD
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtHD = Value
                Catch ex As Exception
                    m_strtxtHD = ""
                End Try
            End Set
        End Property



        '----------------------------------------------------------------
        ' txtNF����
        '----------------------------------------------------------------
        Public Property txtNF() As String
            Get
                txtNF = m_strtxtNF
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtNF = Value
                Catch ex As Exception
                    m_strtxtNF = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' ddlYF_SelectedIndex����
        '----------------------------------------------------------------
        Public Property ddlYF_SelectedIndex() As Integer
            Get
                ddlYF_SelectedIndex = m_intSelectedIndex_ddlYF
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_ddlYF = Value
                Catch ex As Exception
                    m_intSelectedIndex_ddlYF = -1
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLDAPSearch_APRQMin����
        '----------------------------------------------------------------
        Public Property txtLDAPSearch_APRQMin() As String
            Get
                txtLDAPSearch_APRQMin = m_strtxtLDAPSearch_APRQMin
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLDAPSearch_APRQMin = Value
                Catch ex As Exception
                    m_strtxtLDAPSearch_APRQMin = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' txtLDAPSearch_APRQMax����
        '----------------------------------------------------------------
        Public Property txtLDAPSearch_APRQMax() As String
            Get
                txtLDAPSearch_APRQMax = m_strtxtLDAPSearch_APRQMax
            End Get
            Set(ByVal Value As String)
                Try
                    m_strtxtLDAPSearch_APRQMax = Value
                Catch ex As Exception
                    m_strtxtLDAPSearch_APRQMax = ""
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' grdLDAPPageSize����
        '----------------------------------------------------------------
        Public Property grdLDAPPageSize() As Integer
            Get
                grdLDAPPageSize = m_intPageSize_grdLDAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intPageSize_grdLDAP = Value
                Catch ex As Exception
                    m_intPageSize_grdLDAP = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdLDAPCurrentPageIndex����
        '----------------------------------------------------------------
        Public Property grdLDAPCurrentPageIndex() As Integer
            Get
                grdLDAPCurrentPageIndex = m_intCurrentPageIndex_grdLDAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intCurrentPageIndex_grdLDAP = Value
                Catch ex As Exception
                    m_intCurrentPageIndex_grdLDAP = 0
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' grdLDAPSelectedIndex����
        '----------------------------------------------------------------
        Public Property grdLDAPSelectedIndex() As Integer
            Get
                grdLDAPSelectedIndex = m_intSelectedIndex_grdLDAP
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_grdLDAP = Value
                Catch ex As Exception
                    m_intSelectedIndex_grdLDAP = -1
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' rblLDAPSearchAPRQSelectedIndex����
        '----------------------------------------------------------------
        Public Property rblLDAPSearchAPRQSelectedIndex() As Integer
            Get
                rblLDAPSearchAPRQSelectedIndex = m_intSelectedIndex_rblLDAPSearchAPRQ
            End Get
            Set(ByVal Value As Integer)
                Try
                    m_intSelectedIndex_rblLDAPSearchAPRQ = Value
                Catch ex As Exception
                    m_intSelectedIndex_rblLDAPSearchAPRQ = -1
                End Try
            End Set
        End Property

    End Class

End Namespace
