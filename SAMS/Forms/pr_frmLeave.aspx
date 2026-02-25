<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true"
    CodeFile="pr_frmLeave.aspx.cs" Inherits="pr_frmLeave" Title="Leave" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="cc1" %>
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
            border-radius: 3px;
        }
        .customerSerch
        {
            clear: both;
        }
</style>
    <script language="JavaScript" type="text/javascript">
        function pageLoad() {
            $("tbody.alternateRow tr:visible").each(function (index) {
                var $this = $(this);
                if (index & 1) {
                    $this.addClass("odd");
                } else {
                    $this.removeClass("odd");
                }

                //find the current popup
                var popUp = $find('ModelPopup');

                //check it exists so the script won't fail
                if (popUp) {
                    //Add the function below as the event
                    popUp.add_hidden(HidePopupPanel);
                }

            });
            $("#text").keyup(function (e) {
                $("#table1 tr:has(td)").hide();
                var iCounter = 0;
                var sSearchTerm = $("#text").val(); //Get the search box value
                if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
                {
                    $("#table1 tr:has(td)").show();
                    return false;
                }
                $("#table1 tr:has(td)").children().each(function () {
                    var cellText = $(this).text().toLowerCase();
                    if (cellText.indexOf(sSearchTerm.toLowerCase()) >= 0) //Check if data matches
                    {
                        $(this).parent().show();
                        iCounter++;
                        return true;
                    }
                });
            });
        }

        function ValidateForm() {
       
              return true;
          }
        $(document).ready(function () {
        });

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
                <table width="100%">
                    <tr>
                        <td width="5px">
                        </td>
                        <td>
                            <table width="100%">
                                <tbody>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="Label3" runat="server" Text="Location"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlLocation" OnSelectedIndexChanged="ddlLocation_Change" AutoPostBack="true" runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="Label5" runat="server"  Text="Department"></asp:Label>
                                        </td>
                                        <td >
                                            <asp:DropDownList ID="ddlDepartment"  OnSelectedIndexChanged="ddlDepartment_Change" AutoPostBack="true" runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="lblEmployee" runat="server" Text="Employee"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="210px" CssClass="DropList">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            
                                        </td>
                                        <td width="50%">
                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="lblLeaveType" runat="server" Text="Leave Type"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlLeaveType" runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            
                                        </td>
                                        <td width="50%">
                                            
                                        </td>
                                    </tr>
                                     <tr>
                                        <td width="10%">
                                            <asp:Label ID="lblDateFrom" runat="server" Text="Date From"></asp:Label>                                            
                                        </td>
                                        <td width="30%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:25%;">
                                                        <asp:TextBox ID="txtDateFrom" runat="server"></asp:TextBox>
                                                        <cc1:CalendarExtender ID="txtDateFrom_CalendarExtender" runat="server" TargetControlID="txtDateFrom" PopupButtonID="txtDateFrom" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                                    </td>
                                                    <td style="width:75%;">
                                                        <cc1:TimeSelector ID="txtTimeFrom"  runat="server" DisplaySeconds="true" SelectedTimeFormat="TwentyFour" EnableTheming="true">
                                                        </cc1:TimeSelector>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td width="10%">
                                            <asp:Label ID="lblDateTo" runat="server"  Text="Date To"></asp:Label>
                                        </td>
                                        <td width="50%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:25%;">
                                                        <asp:TextBox ID="txtDateTo" runat="server"></asp:TextBox>                                                    
                                                        <cc1:CalendarExtender ID="txtDateTo_CalendarExtender" runat="server" TargetControlID="txtDateTo" PopupButtonID="txtDateTo" Format="dd/MM/yyyy"></cc1:CalendarExtender>                                            
                                                    </td>
                                                    <td style="width:75%;">
                                                        <cc1:TimeSelector ID="txtTimeTo" runat="server" DisplaySeconds="false" SelectedTimeFormat="TwentyFour" EnableTheming="true">
                                                        </cc1:TimeSelector>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="lblComments" runat="server" Text="Comments"></asp:Label>
                                        </td>
                                        <td width="80%" colspan="3">
                                            <asp:TextBox ID="txtComments" Width="80%" runat="server"></asp:TextBox>
                                        </td>                                        
                                    </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>
                <table>
                <tr>
                <td class="customerSerch">
                <input type="text" id="text" name="n" style="width: 250px;" placeholder="Enter Text To Search " />
                </td>
                </tr>
                </table>
                <asp:HiddenField ID="hfLeaveTransactionsID" runat="server" />
                <asp:Repeater ID="rptEmployee" runat="server" onitemcommand="rptEmployee_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">
                                    <td width="15%">
                                        Employee Name
                                    </td>
                                    <td width="10%">
                                         Designation
                                    </td>
                                    <td width="10%">
                                        Leave Type
                                    </td>
                                    <td width="10%">
                                        Date From
                                    </td>
                                    <td width="10%">
                                        Time From
                                    </td>
                                    <td width="10%">
                                        Date To
                                        </td>
                                    <td width="0%">
                                        Time To
                                    </td>
                                    <td width="15%">
                                        Comments
                                    </td>
                                    <td width="10%" colspan="2">
                                        Action
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                             <%# Eval("EmployeeFullName")%>                                
                            </td>
                            <td>
                             <%# Eval("DesignationName")%>
                               
                            </td>
                            <td>
                                <%# Eval("LeaveType")%>
                            </td>
                            <td>                                 
                                 <%# Eval("LeaveFrom") %>
                            </td>
                            <td>
                                <%# Convert.ToDateTime(Eval("LeaveFrom2")).TimeOfDay%>      
                            </td>
                            <td>                                
                                 <%# Eval("LeaveTo") %>
                                </td>
                            <td>                       
                                <%# Convert.ToDateTime(Eval("LeaveTo2")).TimeOfDay%>      
                            </td>
                            <td>
                               <%# Eval("Note")%>         
                            </td>
                            <td>
                                <asp:LinkButton ID="btnEditLeave"  CommandArgument='<%# Eval("LeaveTransactionsID")%>' CommandName="edit" runat="server" ClientIDMode="AutoID" >
                                    Edit
                                </asp:LinkButton>                             
                            </td>
                            <td>
                                <asp:LinkButton ID="btnDeleteLeave"  CommandArgument='<%# Eval("LeaveTransactionsID")%>' CommandName="delete" runat="server" ClientIDMode="AutoID" OnClientClick="javascript:return confirm('Are you sure you want to Delete?');return false;">
                                    Delete
                                </asp:LinkButton>                             
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody> </table>
                    </FooterTemplate>
                </asp:Repeater>
                <table style="margin-top:10px;">
                    <tr>
                    <td width="5px"></td>
                        <td>
                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click">                            
                            <asp:Image ID="imgLeave" runat="server" ImageUrl="../images/btn-save.png" />
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDisCard" runat="server" OnClick="btnDisCard_Click">Discard</asp:LinkButton>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
