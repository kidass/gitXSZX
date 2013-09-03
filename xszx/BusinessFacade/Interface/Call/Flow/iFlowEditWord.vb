Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IFlowEditWord
    '
    ' 功能描述： 
    '     flow_editword.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowEditWord
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_strFlowTypeName_I As String                                        '工作流类型名称
        Private m_strWJBS_I As String                                                '文件标识
        Private m_blnEditMode_I As Boolean                                           '编辑模式
        Private m_blnAutoSave_I As Boolean                                           '是否需要自动保存
        Private m_blnEnforeEdit_I As Boolean                                         '是否定稿后修改?
        Private m_blnTrackRevisions_I As Boolean                                     '文件支持痕迹记录?
        Private m_strGJFileSpec_I As String                                          '当前正在编辑的稿件文件,没有编辑过=""(纯文件名)
        Private m_objNewData_I As System.Collections.Specialized.NameValueCollection '进入稿件编辑时的主文件数据
        Private m_objDataSet_FJ_I As Xydc.Platform.Common.Data.FlowData                 '进入稿件编辑时的附件数据
        Private m_objDataSet_XGWJ_I As Xydc.Platform.Common.Data.FlowData               '进入稿件编辑时的相关文件数据
        Private m_strSPR_I As String                                                 '签批人名称(非自己签批="")
        Private m_strDLR_I As String                                                 '代理人名称
        Private m_strDLRDM_I As String                                               '代理人代码
        Private m_strDLRBMDM_I As String                                             '代理人单位代码
        Private m_blnHasSendOnce_I As Boolean                                        '文件是否发送过?
        Private m_blnCanQSYJ_I As Boolean                                            '当前人员是否可边改边签批意见?
        Private m_blnCanImportGJ_I As Boolean                                        '是否支持导入稿件文件?
        Private m_blnCanExportGJ_I As Boolean                                        '是否支持导出稿件文件?
        Private m_blnCanSelectTGWJ_I As Boolean                                      '是否支持选择投稿文件

        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        '返回方式：True-确定，False-取消
        Private m_blnExitMode_O As Boolean









        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_blnEditMode_I = False
            m_strWJBS_I = ""
            m_strGJFileSpec_I = ""
            m_strFlowTypeName_I = ""
            m_blnAutoSave_I = False
            m_objNewData_I = Nothing
            m_objDataSet_FJ_I = Nothing
            m_objDataSet_XGWJ_I = Nothing
            m_strSPR_I = ""
            m_strDLR_I = ""
            m_strDLRDM_I = ""
            m_strDLRBMDM_I = ""
            m_blnTrackRevisions_I = False
            m_blnHasSendOnce_I = False
            m_blnCanQSYJ_I = False
            m_blnCanImportGJ_I = False
            m_blnCanExportGJ_I = False
            m_blnCanSelectTGWJ_I = False
            m_blnEnforeEdit_I = False

            '初始化输出参数
            m_blnExitMode_O = False

        End Sub

        '----------------------------------------------------------------
        ' 重载父类的析构函数
        '----------------------------------------------------------------
        Public Overloads Sub Dispose()
            MyBase.Dispose()
            Dispose(True)
        End Sub

        '----------------------------------------------------------------
        ' 释放本身资源
        '----------------------------------------------------------------
        Protected Overloads Sub Dispose(ByVal disposing As Boolean)
            If (Not disposing) Then
                Exit Sub
            End If
            If Not (m_objNewData_I Is Nothing) Then
                m_objNewData_I.Clear()
                m_objNewData_I = Nothing
            End If
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowEditWord)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub















        '----------------------------------------------------------------
        ' iWJBS属性
        '----------------------------------------------------------------
        Public Property iWJBS() As String
            Get
                iWJBS = m_strWJBS_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJBS_I = Value
                Catch ex As Exception
                    m_strWJBS_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iEditMode属性
        '----------------------------------------------------------------
        Public Property iEditMode() As Boolean
            Get
                iEditMode = m_blnEditMode_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnEditMode_I = Value
                Catch ex As Exception
                    m_blnEditMode_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iEnforeEdit属性
        '----------------------------------------------------------------
        Public Property iEnforeEdit() As Boolean
            Get
                iEnforeEdit = m_blnEnforeEdit_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnEnforeEdit_I = Value
                Catch ex As Exception
                    m_blnEnforeEdit_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iFlowTypeName属性
        '----------------------------------------------------------------
        Public Property iFlowTypeName() As String
            Get
                iFlowTypeName = m_strFlowTypeName_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strFlowTypeName_I = Value
                Catch ex As Exception
                    m_strFlowTypeName_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iGJFileSpec属性
        '----------------------------------------------------------------
        Public Property iGJFileSpec() As String
            Get
                iGJFileSpec = m_strGJFileSpec_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strGJFileSpec_I = Value
                Catch ex As Exception
                    m_strGJFileSpec_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iAutoSave属性
        '----------------------------------------------------------------
        Public Property iAutoSave() As Boolean
            Get
                iAutoSave = m_blnAutoSave_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnAutoSave_I = Value
                Catch ex As Exception
                    m_blnAutoSave_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iobjNewData属性
        '----------------------------------------------------------------
        Public Property iobjNewData() As System.Collections.Specialized.NameValueCollection
            Get
                iobjNewData = m_objNewData_I
            End Get
            Set(ByVal Value As System.Collections.Specialized.NameValueCollection)
                Try
                    m_objNewData_I = Value
                Catch ex As Exception
                    m_objNewData_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iobjDataSet_FJ属性
        '----------------------------------------------------------------
        Public Property iobjDataSet_FJ() As Xydc.Platform.Common.Data.FlowData
            Get
                iobjDataSet_FJ = m_objDataSet_FJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_FJ_I = Value
                Catch ex As Exception
                    m_objDataSet_FJ_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iobjDataSet_XGWJ属性
        '----------------------------------------------------------------
        Public Property iobjDataSet_XGWJ() As Xydc.Platform.Common.Data.FlowData
            Get
                iobjDataSet_XGWJ = m_objDataSet_XGWJ_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Data.FlowData)
                Try
                    m_objDataSet_XGWJ_I = Value
                Catch ex As Exception
                    m_objDataSet_XGWJ_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iSPR属性
        '----------------------------------------------------------------
        Public Property iSPR() As String
            Get
                iSPR = m_strSPR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strSPR_I = Value
                Catch ex As Exception
                    m_strSPR_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDLR属性
        '----------------------------------------------------------------
        Public Property iDLR() As String
            Get
                iDLR = m_strDLR_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDLR_I = Value
                Catch ex As Exception
                    m_strDLR_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDLRDM属性
        '----------------------------------------------------------------
        Public Property iDLRDM() As String
            Get
                iDLRDM = m_strDLRDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDLRDM_I = Value
                Catch ex As Exception
                    m_strDLRDM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iDLRBMDM属性
        '----------------------------------------------------------------
        Public Property iDLRBMDM() As String
            Get
                iDLRBMDM = m_strDLRBMDM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strDLRBMDM_I = Value
                Catch ex As Exception
                    m_strDLRBMDM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iTrackRevisions属性
        '----------------------------------------------------------------
        Public Property iTrackRevisions() As Boolean
            Get
                iTrackRevisions = m_blnTrackRevisions_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnTrackRevisions_I = Value
                Catch ex As Exception
                    m_blnTrackRevisions_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iHasSendOnce属性
        '----------------------------------------------------------------
        Public Property iHasSendOnce() As Boolean
            Get
                iHasSendOnce = m_blnHasSendOnce_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnHasSendOnce_I = Value
                Catch ex As Exception
                    m_blnHasSendOnce_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCanQSYJ属性
        '----------------------------------------------------------------
        Public Property iCanQSYJ() As Boolean
            Get
                iCanQSYJ = m_blnCanQSYJ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanQSYJ_I = Value
                Catch ex As Exception
                    m_blnCanQSYJ_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCanImportGJ属性
        '----------------------------------------------------------------
        Public Property iCanImportGJ() As Boolean
            Get
                iCanImportGJ = m_blnCanImportGJ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanImportGJ_I = Value
                Catch ex As Exception
                    m_blnCanImportGJ_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCanExportGJ属性
        '----------------------------------------------------------------
        Public Property iCanExportGJ() As Boolean
            Get
                iCanExportGJ = m_blnCanExportGJ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanExportGJ_I = Value
                Catch ex As Exception
                    m_blnCanExportGJ_I = False
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iCanSelectTGWJ属性
        '----------------------------------------------------------------
        Public Property iCanSelectTGWJ() As Boolean
            Get
                iCanSelectTGWJ = m_blnCanSelectTGWJ_I
            End Get
            Set(ByVal Value As Boolean)
                Try
                    m_blnCanSelectTGWJ_I = Value
                Catch ex As Exception
                    m_blnCanSelectTGWJ_I = False
                End Try
            End Set
        End Property





        '----------------------------------------------------------------
        ' oExitMode属性
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

    End Class

End Namespace
