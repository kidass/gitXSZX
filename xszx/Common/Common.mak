#
# Makefile for the Duwamish 7.0 Sample Application, Common Assembly
#

#
# Include Shared definitions
#
!include ..\shared.mak

#
# The target Assembly
#
TARGET=..\$(WEBDIR)\bin\Duwamish7.Common.dll

#
# Referenced Assemblies
#
REFERENCES= \
	-r:System.dll \
	-r:System.Data.dll \
	-r:System.Web.dll \
        -r:System.Xml.dll \
	-r:..\$(WEBDIR)\bin\Duwamish7.SystemFramework.dll

#
# The Sources
#
SOURCES= \
	AssemblyInfo.vb \
	DuwamishConfiguration.vb \
	Data\BookData.vb \
	Data\CategoryData.vb \
	Data\CustomerData.vb \
	Data\OrderData.vb

#
# Everything
#
all	: $(TARGET)
	

$(TARGET) : $(SOURCES)
	vbc.exe $(VBCOPTS) -target:library $(REFERENCES) $(SOURCES) -out:$(TARGET)
