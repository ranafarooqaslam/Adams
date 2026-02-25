<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmGazettedHolidays.aspx.cs" Inherits="pr_frmGazettedHolidays" Title="Gazetted Holidays" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <style>
        .customerSerch input
        {
            -moz-box-shadow: inset 0 0 6px #333;
            -webkit-box-shadow: inset 0 0 6px #333;
            box-shadow: inset 0 0 6px #333;
            -moz-border-radius: 3px;
            height: 26px;
            float: left;
            padding-left: 5px;
            border: none;
            color: #666;
            margin-top: 10px;
            margin-left: 10px;
            border-radius: 3px;
        }
        .customerSerch
        {
            clear: both;
        }
</style>    
<script language="JavaScript" type="text/javascript">

    function pageLoad() {

        //find the current popup
        var popUp = $find('ModelPopup');

        //check it exists so the script won't fail
        if (popUp) {
            //Add the function below as the event
            popUp.add_hidden(HidePopupPanel);
        }
    }

    function HidePopupPanel(source, args) {
        //find the panel associated with the extender
        objPanel = document.getElementById(source._PopupControlID);

        //check the panel exists
        if (objPanel) {
            //set the display attribute, so it remains hidden on postback
            objPanel.style.display = 'none';
        }
    }

    var prm = Sys.WebForms.PageRequestManager.getInstance();
    //Raised before processing of an asynchronous postback starts and the postback request is sent to the server.
    prm.add_beginRequest(BeginRequestHandler);
    // Raised after an asynchronous postback is finished and control has been returned to the browser.
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args) {
        //Shows the modal popup - the update progress
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.show();
        }
    }

    function EndRequestHandler(sender, args) {
        //Hide the modal popup - the update progress
        var popup = $find('<%= modalPopup.ClientID %>');
        if (popup != null) {
            popup.hide();
        }
    }

    </script>
    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
        <ContentTemplate>
            <div id="right_data">
            <div>
                            <asp:UpdateProgress ID="UpdateProgress" runat="server">
                                <ProgressTemplate>
                                    <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                                        Width="31px" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                            <cc1:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
                                PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
                            </cc1:ModalPopupExtender>
                        </div>
                <asp:Panel ID="pnlGazettedContent" runat="server">
                <table width="100%">
                    <tr>
                        <td width="5px">
                            <asp:HiddenField ID="hdnGazettedID" runat="server" />
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                <tr>
                                <td height="20px">
                                </td>
                                </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label2" runat="server"  Text="Date"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtDate" runat="server" Width="100px"></asp:TextBox>
                                            <asp:ImageButton ID="ibDate" runat="server" ImageUrl="~/App_Themes/Granite/Images/date.gif" Width="16px" />
                                            <cc1:CalendarExtender ID="ceDate" runat="server" Format="dd-MMM-yyyy" PopupButtonID="ibDate" TargetControlID="txtDate">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td width="4%">
                                        </td>
                                        <td width="56%">
                                        </td>
                                    </tr>       
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label1" runat="server"  Text="Description"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:TextBox ID="txtDescription" runat="server" Width="250px"></asp:TextBox>
                                        </td>
                                        <td width="4%">
                                        </td>
                                        <td width="56%">
                                        </td>
                                    </tr>                                                                
                                </tbody>
                            </table>                            
                        </td>
                    </tr>
                </table>                 
                <table style="margin-top:10px;">
                    <tr>
                    <td width="5px"></td>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">
                                <asp:Image ID="imgGazetted" runat="server" ImageUrl="../images/btn-save.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                </asp:Panel>
                <asp:Panel ID="pnlGazettedGrid" runat="server">
                    <asp:LinkButton ID="btnShowpnlGazettedContent" OnClick="btnShowpnlGazettedContent_Click" runat="server">Add New</asp:LinkButton>
                <asp:Repeater ID="rptGazetted" runat="server" OnItemCommand="rptGazetted_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">
                                    <td width="40%">
                                        Holiday
                                    </td>
                                    <td width="40%">
                                         Description
                                    </td>                                                              
                                    <td width="20%" colspan="2">
                                        Action
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                             <%# DataBinder.Eval(Container.DataItem, "Date", "{0:dd-MMM-yyyy}")%>                         
                            </td>
                            <td>
                             <%# Eval("Description")%>
                            </td>                                                                                 
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("GazettedHolidayID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("GazettedHolidayID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td>                            
                        </tr>
                    </ItemTemplate>
                    <AlternatingItemTemplate>
                         <tr style="background-color:#E4E4E4">
                            <td>
                             <%# DataBinder.Eval(Container.DataItem, "Date", "{0:dd-MMM-yyyy}")%>
                            </td>
                            <td>
                             <%# Eval("Description")%>
                            </td>                                                                                 
                             <td>
                                <asp:LinkButton ID="btnEditAssets" CommandArgument='<%# Eval("GazettedHolidayID")%>' CommandName="edit"
                                    runat="server" ClientIDMode="AutoID">
                                    Edit
                                </asp:LinkButton>
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDeleteAssets" CommandName="delete" CommandArgument='<%# Eval("GazettedHolidayID")%>'  runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                            Delete
                                </asp:LinkButton>
                            </td>                     
                        </tr>
                    </AlternatingItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
                </asp:Panel>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
