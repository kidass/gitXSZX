Imports System

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：IFlowFujianInfo
    '
    ' 功能描述： 
    '     flow_fujian_info.aspx模块调用接口的定义与处理
    '----------------------------------------------------------------
    <Serializable()> Public Class IFlowFujianInfo
        Inherits Xydc.Platform.BusinessFacade.ICallInterface

        '----------------------------------------------------------------
        '输入参数
        '----------------------------------------------------------------
        Private m_objEditType_I As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType       '输入编辑类型
        Private m_objRow_I As System.Data.DataRow                                                 '输入当前行数据
        Private m_strFlowTypeName_I As String                                                     '输入工作流类型
        Private m_strWJBS_I As String                                                             '输入文件标识
        Private m_strWJXH_I As String                                                             '输入文件序号
        Private m_strBDWJ_I As String                                                             '输入文件位置(WEB本地文件路径)
        Private m_strWJSM_I As String                                                             '输入文件说明
        Private m_strWJYS_I As String                                                             '输入文件页数
        Private m_strWJWZ_I As String                                                             '输入文件位置(FTP文件路径)
        Private m_blnTrackRevisions_I As Boolean                                                  '文件支持痕迹记录?
        Private m_blnAutoSave_I As Boolean                                                        '退出时自动保存附件
        Private m_blnEnforeEdit_I As Boolean                                                      '是否定稿后修改?



        '----------------------------------------------------------------
        '输出参数
        '----------------------------------------------------------------
        Private m_blnExitMode_O As Boolean                                                        '返回方式：true-确定,false-取消
        Private m_strWJXH_O As String                                                             '输出序号
        Private m_strBDWJ_O As String                                                             '输出文件位置(WEB本地文件路径)
        Private m_strWJSM_O As String                                                             '输出文件说明
        Private m_strWJYS_O As String                                                             '输出文件页数
        Private m_strWJWZ_O As String                                                             '输出文件位置(FTP文件路径)










        '----------------------------------------------------------------
        ' 构造函数
        '----------------------------------------------------------------
        Public Sub New()
            MyBase.New()

            '初始化父类参数
            MyBase.iInterfaceType = ICallInterface.enumInterfaceType.InputAndOutput

            '初始化输入参数
            m_objEditType_I = Common.Utilities.PulicParameters.enumEditType.eSelect
            m_objRow_I = Nothing
            m_strFlowTypeName_I = ""
            m_strWJBS_I = ""
            m_strWJXH_I = ""
            m_strBDWJ_I = ""
            m_strWJSM_I = ""
            m_strWJYS_I = ""
            m_strWJWZ_I = ""
            m_blnTrackRevisions_I = False
            m_blnAutoSave_I = False
            m_blnEnforeEdit_I = False

            '初始化输出参数
            m_blnExitMode_O = False
            m_strWJXH_O = ""
            m_strBDWJ_O = ""
            m_strWJSM_O = ""
            m_strWJYS_O = ""
            m_strWJWZ_O = ""

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
        End Sub

        '----------------------------------------------------------------
        ' 安全释放本身资源
        '----------------------------------------------------------------
        Public Overloads Shared Sub SafeRelease(ByRef obj As Xydc.Platform.BusinessFacade.IFlowFujianInfo)
            Try
                If Not (obj Is Nothing) Then
                    obj.Dispose()
                End If
            Catch ex As Exception
            End Try
            obj = Nothing
        End Sub











        '----------------------------------------------------------------
        ' iEditType属性
        '----------------------------------------------------------------
        Public Property iEditType() As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType
            Get
                iEditType = m_objEditType_I
            End Get
            Set(ByVal Value As Xydc.Platform.Common.Utilities.PulicParameters.enumEditType)
                Try
                    m_objEditType_I = Value
                Catch ex As Exception
                    m_objEditType_I = Common.Utilities.PulicParameters.enumEditType.eSelect
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
        ' iRow属性
        '----------------------------------------------------------------
        Public Property iRow() As System.Data.DataRow
            Get
                iRow = m_objRow_I
            End Get
            Set(ByVal Value As System.Data.DataRow)
                Try
                    m_objRow_I = Value
                Catch ex As Exception
                    m_objRow_I = Nothing
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJXH属性
        '----------------------------------------------------------------
        Public Property iWJXH() As String
            Get
                iWJXH = m_strWJXH_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJXH_I = Value
                Catch ex As Exception
                    m_strWJXH_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iBDWJ属性
        '----------------------------------------------------------------
        Public Property iBDWJ() As String
            Get
                iBDWJ = m_strBDWJ_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strBDWJ_I = Value
                Catch ex As Exception
                    m_strBDWJ_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJSM属性
        '----------------------------------------------------------------
        Public Property iWJSM() As String
            Get
                iWJSM = m_strWJSM_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJSM_I = Value
                Catch ex As Exception
                    m_strWJSM_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJYS属性
        '----------------------------------------------------------------
        Public Property iWJYS() As String
            Get
                iWJYS = m_strWJYS_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJYS_I = Value
                Catch ex As Exception
                    m_strWJYS_I = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' iWJWZ属性
        '----------------------------------------------------------------
        Public Property iWJWZ() As String
            Get
                iWJWZ = m_strWJWZ_I
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJWZ_I = Value
                Catch ex As Exception
                    m_strWJWZ_I = ""
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

        '----------------------------------------------------------------
        ' oWJXH属性
        '----------------------------------------------------------------
        Public Property oWJXH() As String
            Get
                oWJXH = m_strWJXH_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJXH_O = Value
                Catch ex As Exception
                    m_strWJXH_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oBDWJ属性
        '----------------------------------------------------------------
        Public Property oBDWJ() As String
            Get
                oBDWJ = m_strBDWJ_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strBDWJ_O = Value
                Catch ex As Exception
                    m_strBDWJ_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oWJSM属性
        '----------------------------------------------------------------
        Public Property oWJSM() As String
            Get
                oWJSM = m_strWJSM_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJSM_O = Value
                Catch ex As Exception
                    m_strWJSM_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oWJYS属性
        '----------------------------------------------------------------
        Public Property oWJYS() As String
            Get
                oWJYS = m_strWJYS_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJYS_O = Value
                Catch ex As Exception
                    m_strWJYS_O = ""
                End Try
            End Set
        End Property

        '----------------------------------------------------------------
        ' oWJWZ属性
        '----------------------------------------------------------------
        Public Property oWJWZ() As String
            Get
                oWJWZ = m_strWJWZ_O
            End Get
            Set(ByVal Value As String)
                Try
                    m_strWJWZ_O = Value
                Catch ex As Exception
                    m_strWJWZ_O = ""
                End Try
            End Set
        End Property

    End Class

End Namespace
