<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="frmCustomerPromotionAssignment.aspx.cs" Inherits="Forms_frmCustomerPromotionAssignment"
    Title="SAMS :: Promotion Class Assignement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="cphPage">
    <script type="text/javascript" language="javascript">
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

        function pageLoad() {
            var $checkBoxAll = $('[id$="cbAllCustomer"]'); // assign main checkbox into a variable
            var $ChkBoxlst = $("table.checkboxlst input:checkbox"); // assign all checkboxes in to a variable
            $checkBoxAll.click(function () { // trigger click event when main checkbox will be clicked
                $ChkBoxlst
                .attr('checked', $checkBoxAll // attr() method checks all checkboxes
                .is(':checked'));
            });
            $ChkBoxlst.click( // trigger an event click
            function (e) {
                if (!$(this)[0].checked) { // check if all checkboxes is checked
                    $checkBoxAll.attr("checked", false); // un check the all checkbxes using attr() method, passing false as a second parameter
                }
            });
        }
    </script>
    <div id="right_data">
                <div>
    <asp:UpdateProgress ID="UpdateProgress" runat="server">
        <ProgressTemplate>
            <asp:ImageButton ID="ImageButton10" runat="server" Height="28px" ImageUrl="~/App_Themes/Granite/Images/image003.gif"
                Width="31px" />
        </ProgressTemplate>
    </asp:UpdateProgress>
    <ajaxToolkit:ModalPopupExtender ID="modalPopup" runat="server" TargetControlID="UpdateProgress"
        PopupControlID="UpdateProgress" BackgroundCssClass="modalBackground">
    </ajaxToolkit:ModalPopupExtender>
</div>
        <asp:UpdatePanel ID="upPromotion" runat="server">
            <ContentTemplate>
                <table width="100%">
                    <tr>
                        <td style="width: 100px">
                            <table>
                                <tr>
                                    <td style="width: 100px">
                                        <strong>
                                            <asp:Label ID="Label7" runat="server" CssClass="lblbox" Text="Location" Width="77px"></asp:Label></strong>
                                    </td>
                                    <td style="width: 100px">
                                        <asp:DropDownList ID="DrpDistributor" runat="server" Width="205px"
                                            AutoPostBack="true" OnSelectedIndexChanged="DrpDistributor_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <strong>
                                            <asp:Label ID="Label1" runat="server" Text="Route"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 100px">
                                        <asp:DropDownList ID="ddlRoute" runat="server" Width="205px"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlRoute_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        <strong>
                                            <asp:Label ID="Label2" runat="server" Text="Promotion Class"></asp:Label>
                                        </strong>
                                    </td>
                                    <td style="width: 100px">
                                        <asp:DropDownList ID="ddlPromoiton" runat="server" Width="205px"
                                            AutoPostBack="true" OnSelectedIndexChanged="ddlPromoiton_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                        
                                    </td>
                                    <td style="width: 100px">
                                        <strong>
                                            <asp:CheckBox ID="cbAllCustomer" runat="server" Text="Customer"/>
                                        </strong>
                                        <br />
                                        <asp:Panel ID="pnlCustomer" runat="server" Width="400px" Height="270px" ScrollBars="Vertical"
                                            BorderWidth="1px" BorderStyle="Groove" BorderColor="Silver">
                                            <asp:CheckBoxList ID="cblCustomer" runat="server" RepeatDirection="Vertical"
                                                CssClass="checkboxlst">
                                            </asp:CheckBoxList>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 100px">
                                    </td>
                                    <td style="width: 100px">
                                        <asp:Button ID="btnSave" runat="server" Font-Size="8pt" OnClick="btnSave_Click" Text="Save"
                                        ValidationGroup="vg" Width="89px" CssClass="Button" />
                                    <asp:Button ID="btnCancel" runat="server" Font-Size="8pt" Text="Cancel" Width="91px"
                                        CssClass="Button" />
                                    </td>
                                </tr>
                            </table>
                            

                        </td>
                    </tr>
                </table>                
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
