Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' �����ռ䣺Xydc.Platform.BusinessFacade
    ' ����    ��IDmxzZzry
    '
    ' ���������� 
    '     dmxz_zzry.aspxģ����ýӿڵĶ����봦��
    '----------------------------------------------------------------
    <Serializable()> Public Class IDmxzZzry
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnDifferentFrame_I As Boolean       '����֡�뵱ǰ֡��ͬ����֡���ã�
        Private m_blnSelectMode_I As Boolean           'ѡ��ʽ��false-��Ա(��ѡ����Ա)��true-��Χ(����ѡ����Ա�����š���Χ)
        Private m_strInputList_I As String             '������Ա�б�
        Private m_blnMultiSelect_I As Boolean          '���Զ���ѡ��?false-����,true-��(ȱʡ)
        Private m_blnSelectFFFW_I As Boolean           '����ѡ��Χ?false-����,true-��(ȱʡ)
        Private m_blnSelectBMMC_I As Boolean           '����ѡ����?false-����,true-��(ȱʡ)
        Private m_blnAllowInput_I As Boolean           '�����ֹ�����?false-����,true-��(ȱʡ)
        Private m_blnAllowNull_I As Boolean            '���������?false-����,true-��(ȱʡ)
        Private m_blnRestrictList_I As Boolean         '������Ա��Ϣ����?false-������(ȱʡ),true-����
        Private m_strRestrictListSQL_I As String       '��Ա��Ϣ�б����Ƶ�SQL���
        Private m_blnSendRestrict_I As Boolean         '�Ƿ�򿪷�������?false-����(ȱʡ),true-��
        Private m_strCurrentBlr_I As String            '��ǰ�����˵���Ա����
        Private m_strCurrentBlr_Dlr_I As String        '��ǰ�����˵Ĵ���������
        Private m_strWeituoren_I As String             '��ǰ��������Weituorenί��������ҵ��(����)

        '----------------------------------------------------------------
        '�������
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean             '�˳���ʽ��True-ȷ����False-ȡ��
        Private m_strRYList_O As String                '��ѡ�����Ա�б�,��ϵͳָ���ķָ����ָ�(CharSeparate)
        Private m_objDataSet_O As System.Data.DataSet  '��ѡʱ���ص����ݼ�









        '----------------------------------------------------------------
        ' ���캯��
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '��ʼ���������
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '��ʼ���������
            m_blnSelectMode_I = True
            m_strInputList_I = ""
            m_blnMultiSelect_I = True
            m_blnSelectFFFW_I = True
            m_blnSelectBMMC_I = True
            m_blnAllowInput_I = True
            m_blnAllowNull_I = True
            m_blnRestrictList_I = False
            m_strRestrictListSQL_I = ""
            m_blnSendRestrict_I = False
            m_strCurrentBlr_I = ""
            m_strCurrentBlr_Dlr_I = ""
            m_strWeituoren_I = ""
            m_blnDifferentFrame_I = False

            '��ʼ���������
            m_blnExitMode_O = False
            m_strRYList_O = ""
            m_objDataSet_O = Nothing

        End Sub

        '----------------------------------------------------------------
        ' ���ظ������������
        '----------------------------------------------------------------
        Public Overloads Sub Dispose()
            MyBase.Dispose()
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' �ͷű�����Դ
        '----------------------------------------------------------------
        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
            '�ͷű�����Դ
            'If Not (m_objDataSet_O Is Nothing) Then
            '    m_objDataSet_O.Dispose()
            '    m_objDataSet_O = Nothing
            'End If

            Try
                If Not (m_objDataSet_O Is Nothing) Then
                    m_objDataSet_O.Dispose()
                    m_objDataSet_O = Nothing
                End If
            Catch ex As Exception
            End Try


        End Sub

        '----------------------------------------------------------------
        ' ��ȫ�ͷű�����Դ
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IDmxzZzry)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub












        '----------------------------------------------------------------
        ' iSelectMode����
        '----------------------------------------------------------------
        Public Property iSelectMode() As Boolean
            Get
                iSelectMode = m_blnSelectMode_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSelectMode_I = Value
                Catch ex As Exception
                    m_blnSelectMode_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iRenyuanList����
        '----------------------------------------------------------------
        Public Property iRenyuanList() As String
            Get
                iRenyuanList = m_strInputList_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strInputList_I = Value
                Catch ex As Exception
                    m_strInputList_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iMultiSelect����
        '----------------------------------------------------------------
        Public Property iMultiSelect() As Boolean
            Get
                iMultiSelect = m_blnMultiSelect_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnMultiSelect_I = Value
                Catch ex As Exception
                    m_blnMultiSelect_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSelectFFFW����
        '----------------------------------------------------------------
        Public Property iSelectFFFW() As Boolean
            Get
                iSelectFFFW = m_blnSelectFFFW_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSelectFFFW_I = Value
                Catch ex As Exception
                    m_blnSelectFFFW_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSelectBMMC����
        '----------------------------------------------------------------
        Public Property iSelectBMMC() As Boolean
            Get
                iSelectBMMC = m_blnSelectBMMC_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSelectBMMC_I = Value
                Catch ex As Exception
                    m_blnSelectBMMC_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iAllowInput����
        '----------------------------------------------------------------
        Public Property iAllowInput() As Boolean
            Get
                iAllowInput = m_blnAllowInput_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAllowInput_I = Value
                Catch ex As Exception
                    m_blnAllowInput_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iAllowNull����
        '----------------------------------------------------------------
        Public Property iAllowNull() As Boolean
            Get
                iAllowNull = m_blnAllowNull_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAllowNull_I = Value
                Catch ex As Exception
                    m_blnAllowNull_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iRestrictRenyuanList����
        '----------------------------------------------------------------
        Public Property iRestrictRenyuanList() As Boolean
            Get
                iRestrictRenyuanList = m_blnRestrictList_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnRestrictList_I = Value
                Catch ex As Exception
                    m_blnRestrictList_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iRestrictRenyuanListSQL����
        '----------------------------------------------------------------
        Public Property iRestrictRenyuanListSQL() As String
            Get
                iRestrictRenyuanListSQL = m_strRestrictListSQL_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRestrictListSQL_I = Value
                Catch ex As Exception
                    m_strRestrictListSQL_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSendRestrict����
        '----------------------------------------------------------------
        Public Property iSendRestrict() As Boolean
            Get
                iSendRestrict = m_blnSendRestrict_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnSendRestrict_I = Value
                Catch ex As Exception
                    m_blnSendRestrict_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCurrentBlr����
        '----------------------------------------------------------------
        Public Property iCurrentBlr() As String
            Get
                iCurrentBlr = m_strCurrentBlr_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strCurrentBlr_I = Value
                Catch ex As Exception
                    m_strCurrentBlr_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCurrentBlrDlr����
        '----------------------------------------------------------------
        Public Property iCurrentBlrDlr() As String
            Get
                iCurrentBlrDlr = m_strCurrentBlr_Dlr_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strCurrentBlr_Dlr_I = Value
                Catch ex As Exception
                    m_strCurrentBlr_Dlr_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWeiTuoRen����
        '----------------------------------------------------------------
        Public Property iWeiTuoRen() As String
            Get
                iWeiTuoRen = m_strWeituoren_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWeituoren_I = Value
                Catch ex As Exception
                    m_strWeituoren_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDifferentFrame����
        '----------------------------------------------------------------
        Public Property iDifferentFrame() As Boolean
            Get
                iDifferentFrame = m_blnDifferentFrame_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnDifferentFrame_I = Value
                Catch ex As Exception
                    m_blnDifferentFrame_I = False
                End Try
            End Set
        End Property




        '----------------------------------------------------------------
        ' oExitMode����
        '----------------------------------------------------------------
        Public Property oExitMode() As Boolean
            Get
                oExitMode = m_blnExitMode_O
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnExitMode_O = Value
                Catch ex As Exception
                    m_blnExitMode_O = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oRenyuanList����
        '----------------------------------------------------------------
        Public Property oRenyuanList() As String
            Get
                oRenyuanList = m_strRYList_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strRYList_O = Value
                Catch ex As Exception
                    m_strRYList_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oDataSet����
        '----------------------------------------------------------------
        Public Property oDataSet() As System.Data.DataSet
            Get
                oDataSet = m_objDataSet_O
            End Get
            Set(ByVal Value As System.Data.DataSet)
                Try
                    m_objDataSet_O = Value
                Catch ex As Exception
                    m_objDataSet_O = Nothing
                End Try
            End Set
        End Property

    End Class

End Namespace
