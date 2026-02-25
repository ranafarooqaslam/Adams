<%@ Page Language="C#" MasterPageFile="~/Forms/PageMaster.master" AutoEventWireup="true" CodeFile="pr_frmAttendance.aspx.cs" Inherits="pr_frmAttendance" Title="Attendance" %>
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
            margin-left: 10px;
            border-radius: 3px;
        }
        .customerSerch
        {
            clear: both;
        }
        
         .RedRow
        {
            background-color: #FF9900;       
            color: White;     
        }
        .WhiteRow
        {
            background-color: White;            
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
                                        <td width="50%">
                                            <asp:DropDownList ID="ddlDepartment"  OnSelectedIndexChanged="ddlDepartment_Change" AutoPostBack="true" runat="server" Width="209px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="10%">
                                            <asp:Label ID="lblEmployee" runat="server" Text="Employee"></asp:Label>
                                        </td>
                                        <td width="30%">
                                            <asp:DropDownList ID="ddlEmployee" runat="server" Width="209px" 
                                                AutoPostBack="True" onselectedindexchanged="ddlEmployee_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td width="10%">
                                            <asp:HiddenField ID="hfAttendanceID" runat="server"/>
                                        </td>
                                        <td width="50%">
                                            
                                        </td>
                                    </tr>                                 
                                    <tr>
                                        <td width="10%">
                                            <label> Attendance Date</label>
                                        </td>
                                        <td width="30%">
                                              <cc1:CalendarExtender ID="txtDate_CalendarExtender" runat="server" TargetControlID="txtDate" PopupButtonID="txtDate" Format="dd-MMM-yyyy"></cc1:CalendarExtender>
                                              <asp:TextBox ID="txtDate" Width="210px"  runat="server"></asp:TextBox>     
                                        </td>
                                        <td width="10%">
                                            
                                        </td>
                                        <td width="50%">
                                            <table width="100%">
                                                <tr>
                                                    <td style="width:50%">
                                                       <asp:RadioButtonList ID="rblTime" runat="server" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="0" Text="Time In (HH:MM)"></asp:ListItem>
                                                            <asp:ListItem Value="1" Text="Time Out (HH:MM)"></asp:ListItem>
                                                       </asp:RadioButtonList>
                                                    </td>
                                                    <td style="width:20%" align="center">
                                                        <cc1:TimeSelector ID="tsTime" Width="100%" runat="server" 
                                                            DisplaySeconds="false" SelectedTimeFormat="TwentyFour" EnableTheming="true">
                                                        </cc1:TimeSelector>
                                                    </td>
                                                    <td style="width:30%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>                                    
                                </tbody>
                            </table>
                        </td>
                    </tr>
                </table>            
                <table>
                <tr class="customerSerch">
                <td >
                <input type="text" id="text" name="n" style="width: 250px;" placeholder="Enter Text To Search " />
                </td>
                <td>                  
                </td>
                <td>
                   
                </td>
                </tr>
                </table>

                <asp:Repeater ID="rptEmployee" runat="server" 
                    onitemcommand="rptEmployee_ItemCommand">
                    <HeaderTemplate>
                        <table width="100%" style="margin-top:10px;">
                            <thead>
                                <tr class="tblheading">
                                    <td width="30%">
                                        Employee Name
                                    </td>
                                    <td width="25%">
                                         Designation
                                    </td>
                                    <td width="10%">
                                        Date
                                    </td>
                                    <td width="10%">
                                       Time Of Date
                                    </td>
                                    <td width="10%">
                                       Time(In/Out)
                                    </td>
                                    <td width="15%" colspan="3" align="center">
                                        Action
                                    </td>
                                </tr>
                            </thead>
                            <tbody id="table1">
                    </HeaderTemplate>
                    <ItemTemplate>
                            <tr class="<%#SetClass(Convert.ToBoolean(Eval("IsLate")))%>">
                            <td>
                             <%# Eval("EmployeeFullName")%>                             
                             <asp:HiddenField ID="hfDayofMonth" runat="server" Value='<%# Eval("DayofMonth")%>' />
                            </td>
                            <td>
                             <%# Eval("DesignationName")%>
                            </td>
                             <td>
                             <%# DataBinder.Eval(Container.DataItem, "DayofMonth", "{0:dd-MMMM-yyyy}")%>
                            </td>
                            <td>         
                                 <%# Eval("TimeOfDay")%>
                            </td>                            
                            <td>         
                                 <%# Eval("TimeInOut")%>
                            </td>                            
                            <td>                                
                                <asp:LinkButton ID="btnEdit" CommandName="edit" CommandArgument='<%# Eval("AttendanceID")%>'  runat="server" ClientIDMode="AutoID" ToolTip="Edit">
                                    Edit    
                                </asp:LinkButton>
                            </td>   
                             <td>                                
                                <asp:LinkButton ID="btnDelete" CommandName="del" CommandArgument='<%# Eval("AttendanceID")%>'  runat="server" ClientIDMode="AutoID" ToolTip="Delete" OnClientClick="javascript:return confirm('Are you sure you want to delete?');return false;">
                                    Delete
                                </asp:LinkButton>
                            </td>             
                            <td>                                
                                <asp:LinkButton ID="btnRemoveLate" CommandName="remove" CommandArgument='<%# Eval("AttendanceID")%>'  runat="server" ClientIDMode="AutoID" ToolTip="Remove Late" OnClientClick="javascript:return confirm('Are you sure you want to remove Late?');return false;">
                                    <%#GetAction(Convert.ToBoolean(Eval("IsLate")))%>
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
                            <asp:LinkButton ID="btnSave" runat="server" OnClick="btnSave_Click"><img src="../images/btn-save.png" /></asp:LinkButton>
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
