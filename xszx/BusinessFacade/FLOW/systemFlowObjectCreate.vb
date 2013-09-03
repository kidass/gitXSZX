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

Namespace Xydc.Platform.BusinessFacade

    '----------------------------------------------------------------
    ' 命名空间：Xydc.Platform.BusinessFacade
    ' 类名    ：ISystemFlowObjectCreate
    '
    ' 功能描述：
    '     ISystemFlowObject创建接口
    '----------------------------------------------------------------
    Public Interface ISystemFlowObjectCreate

        'ISystemFlowObject对象创建器
        Function Create(ByVal strFlowType As String, ByVal strFlowTypeName As String) As Xydc.Platform.BusinessFacade.systemFlowObject

    End Interface

End Namespace
