<%@ Page Title="Dispatch Note View" Language="C#" MasterPageFile="~/Forms/PageMaster.master"
    AutoEventWireup="true" CodeFile="frmLoadPassEntry.aspx.cs" Inherits="Forms_LoadPassEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cphHeadPage" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cphPage" runat="Server">


 <script type="text/javascript">
     function pageLoad() {

         $("#txtSearch").keyup(function (e) {
             $("#tblLoadpass tr:has(td)").hide();
             var iCounter = 0;
             var sSearchTerm = $("#txtSearch").val(); //Get the search box value
             if (sSearchTerm.length == 0) //if nothing is entered then show all the rows.
             {
                 $("#tblLoadpass tr:has(td)").show();
                 return false;
             }
             $("#tblLoadpass tr:has(td)").children().each(function () {
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
  

  </script>

    <div id="right_data">
        <div>
            <table width="100%">
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 914px">
                                            <%--<asp:Label ID="ltrAdd" runat="server"></asp:Label>--%>

                                               <input type="text" id="txtSearch" name="n" style="width: 500px; height: 18px; margin-left: 0px;"
                                    placeholder="Enter Text To Search "/>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 914px">
                                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                                <tr>
                                                    <td class="tblhead" style="width: 15%;">
                                                        <asp:Label ID="lblID" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                            Text="DN Issue Date" Width="100%" BackColor="#006699"></asp:Label>
                                                    </td>
                                                    <td class="tblhead" style="width: 10%;">
                                                        <asp:Label ID="lblLocation" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                            Text="Document No" Width="100%" BackColor="#006699" Style="margin-left: 0px"></asp:Label>
                                                    </td>
                                                    <td class="tblhead" style="width: 15%;">
                                                        <asp:Label ID="Label1" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                            Text="DeliveryMan" Width="100%" BackColor="#006699"></asp:Label>
                                                    </td>
                                                    <td class="tblhead" style="width: 15%;">
                                                        <asp:Label ID="lblDocumentDate" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                            Text="Route" Width="100%" BackColor="#006699"></asp:Label>
                                                    </td>
                                                    <td class="tblhead" style="width: 10%;">
                                                        <asp:Label ID="lblUserName" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                            Text="Vehicle No" Width="100%" BackColor="#006699"></asp:Label>
                                                    </td>
                                                    <td class="tblhead" style="width: 25%;">
                                                        <asp:Label ID="lblAmount" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                            Text="Customer" Width="100%" BackColor="#006699"></asp:Label>
                                                    </td>
                                                    <td class="tblhead" style="width: 10%;">
                                                        <asp:Label ID="lblAction" runat="server" ForeColor="White" Height="100%" Font-Bold="True"
                                                            Text="Action" Width="100%" BackColor="#006699"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <asp:Repeater ID="rPurchaseOrder" runat="server" OnItemCommand="rPurchaseOrder_ItemCommand">
                                                <ItemTemplate>
                                                    <div style="background: #EFF3FB">
                                                        <table id="tblLoadpass" width="100%" border="0" cellspacing="0" cellpadding="2">
                                                            <tr>
                                                                <td style="width: 15%;">
                                                                    <%# Eval("DOCUMENT_DATE")%>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <%# Eval("LOADPASS_ID")%>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <%# Eval("USER_NAME")%>
                                                                </td>
                                                                <td style="width: 15%;">
                                                                    <%# Eval("AREA_NAME")%>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <%# Eval("[VEHICLE_NO]")%>
                                                                </td>
                                                                <td style="width: 25%;">
                                                                    <%# Eval("[CUSTOMER_NAME]")%>
                                                                </td>
                                                                <td style="width: 10%;">
                                                                    <asp:LinkButton ID="btnLoad" runat="server" CommandName="Load" CommandArgument='<%# Eval("LOADPASS_ID") %>'
                                                                        ToolTip="Close" Text="Edit">
                                                                    </asp:LinkButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </ItemTemplate>
                                                <AlternatingItemTemplate>
                                                    <div style="background: #ffffff">
                                                        <table width="100%" border="0" cellspacing="0" cellpadding="2">
                                                            <td style="width: 15%;">
                                                                <%# Eval("DOCUMENT_DATE")%>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <%# Eval("LOADPASS_ID")%>
                                                            </td>
                                                            <td style="width: 15%;">
                                                                <%# Eval("USER_NAME")%>
                                                            </td>
                                                            <td style="width: 15%;">
                                                                <%# Eval("AREA_NAME")%>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <%# Eval("[VEHICLE_NO]")%>
                                                            </td>
                                                            <td style="width: 25%;">
                                                                <%# Eval("[CUSTOMER_NAME]")%>
                                                            </td>
                                                            <td style="width: 10%;">
                                                                <asp:LinkButton ID="btnLoad" runat="server" CommandName="Load" CommandArgument='<%# Eval("LOADPASS_ID") %>'
                                                                    ToolTip="Close" Text="Edit" >
                                                                </asp:LinkButton>
                                                            </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </AlternatingItemTemplate>
                                            </asp:Repeater>
                                            <table width="100%">
                                                <tr>
                                                    <td align="right" width="56%">
                                                        <asp:LinkButton ID="linkbtnprev" runat="server" Enabled="False" OnClick="linkbtnprev_Click"
                                                            CausesValidation="false" Visible="false" Style="color: #6C5A10;">Previous</asp:LinkButton>
                                                        &nbsp;<asp:LinkButton ID="linkbtnnext" runat="server" Enabled="False" OnClick="linkbtnnext_Click"
                                                            CausesValidation="false" Visible="false" Style="color: #6C5A10;">Next</asp:LinkButton>
                                                    </td>
                                                    <td align="right" width="44%">
                                                        <asp:Label ID="lblCurrentPageNo" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                        <asp:Label ID="lblOf" runat="server" Text="Of" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                        <asp:Label ID="lblTotalNoOfPages" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                        <asp:Label ID="lblDummy" runat="server" Text="| Total Records:" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                        <asp:Label ID="lblTotalNoOfRecords" runat="server" Text="" Visible="false" Style="color: #6C5A10;"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
