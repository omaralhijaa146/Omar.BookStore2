

$(

    function () {
        const l = abp.localization.getResource('BookStore2');
        const myWidgetManager = new abp.WidgetManager({
            wrapper: '#WidgetsArea', filterCallback: function () {
                return $("#FilterForm").serialize();
            }
        });

        myWidgetManager.init();

        $("#FilterForm").on("submit", function (e) {

            e.preventDefault();
            myWidgetManager.refresh();
        });

        $('#next').click(function () {
            let skip = parseInt($('#skipCount').val()) || 0;
            let max = parseInt($('#maxResultCount').val()) || 10;
            $('#skipCount').val(skip + max);
            myWidgetManager.refresh();
        });

        $('#prev').click(function () {
            let skip = parseInt($('#skipCount').val()) || 0;
            let max = parseInt($('#maxResultCount').val()) || 10;
            $('#skipCount').val(Math.max(skip - max, 0));
            myWidgetManager.refresh();
        });

        abp.widgets.BooksListWidgetClintSideRefresh = function ($wrapper) {

           var getFilters = function () {
               var formData = $('#ClientSideFilterForm').serializeArray();
               var filters = {};

               formData.forEach(function (f) {
                   filters[f.name] = f.value;
               });
               return filters;
           };

           var refresh = function (filters) {
               omar.bookStore2.books.book.getList(filters).then(function (result) {

                   var bookList = $($wrapper).find('#book-list');
                   bookList.empty();

                   console.log(result);
                   result.items.forEach(function (book) {
                       

                   bookList.append(`<tr>
                           <th>${book.name}</th>
                           <th>${l("Enum:BookType."+book.type)}</th>
                           <th>${ luxon.DateTime.fromISO(
                               book.publishDate, { locale: abp.localization.currentCulture.name }
                           ).toLocaleString()}</th>
                           <th>${book.price}</th>
                           <th>${book.authorName}</th>
                       </tr>`);
                     });


            });

           };

            var init = function () {

                var filters = getFilters();
                refresh(filters);
            };

return {
    init: init,
    refresh: refresh,
    getFilters: getFilters
};
        };

        var widget = abp.widgets.BooksListWidgetClintSideRefresh("#WidgetsAreaClientSide");
        widget.init();
        var filters ={ };
$("#ClientSideFilterForm").on("submit", function (e) {
    e.preventDefault();
   
    filters = widget.getFilters();
    widget.refresh(filters);

});


        $('#nextClientSide').click(function () {
            let skip = parseInt($('#skipCountClientSide').val()) || 0;
            let max = parseInt($('#maxResultCountClientSide').val()) || 10;
            $('#skipCountClientSide').val(skip + max);
            filters = widget.getFilters();
            widget.refresh(filters);
        });

        $('#prevClientSide').click(function () {
            let skip = parseInt($('#skipCountClientSide').val()) || 0;
            let max = parseInt($('#maxResultCountClientSide').val()) || 10;
            $('#skipCountClientSide').val(Math.max(skip - max, 0));
            filters = widget.getFilters();
            widget.refresh(filters);
        });

        
        const createModal = new abp.ModalManager(abp.appPath + "Books/CreateModal");

        const editModal = new abp.ModalManager(abp.appPath + "Books/EditModal");

        const showAuthor = abp.setting.get('BookStore2.ShowBookAuthor');

        const dataTable = $("#BooksTable").DataTable(

            abp.libs.datatables.normalizeConfiguration(


                {

                    serverSide: true,
                    paging: true,
                    order: [[1, "asc"]],
                    searching: false,
                    scrollX: true,
                    ajax: abp.libs.datatables.createAjax(omar.bookStore2.books.book.getList),
                    columnDefs: [

                        {
                            title: l("Actions"),
                            rowAction: {
                                items: [

                                    {
                                        text: l("Edit"),
                                        visible: abp.auth.isGranted("BookStore2.Books.Edit"),
                                        action: function (data) {
                                            editModal.open({ id: data.record.id });

                                        }
                                    },
                                    {
                                        text: l("Delete"),
                                        visible: abp.auth.isGranted("BookStore2.Books.Delete"),
                                        confirmMessage: function (data) {
                                            
                                            return l("BookDeletionConfirmationMessage",data.record.name);
                                        
                                        },
                                        action: function (data) {
                                            omar.bookStore2.books.book.delete(data.record.id).then(function () {
                                                abp.notify.info(l("SuccessfullyDeleted"));
                                                dataTable.ajax.reload();
                                            });
                                        }
                                    }

                                ]
                            }
                        },
                        {
                            title: l('Name'),
                            data: "name"
                        },
                        {
                            title: l("Author"),
                            data: "authorName",
                            visible: showAuthor
                        },
                        {
                            title: l("Type"),
                            data: "type",
                            render: function (data) {
                                return l("Enum:BookType." + data)
                            }
                        },
                        {
                            title: l("PublishDate"),
                            data: "publishDate",
                            render: function (data) {
                                return luxon.DateTime.fromISO(
                                    data, { locale: abp.localization.currentCulture.name }
                                ).toLocaleString();
                            }
                        },
                        {
                            title: l("Price"),
                            data: "price"
                        },

                        {
                            title: l("CreationTime"),
                            data: "creationTime",
                            render: function (data) {

                                return luxon.DateTime.fromISO(
                                    data, { locale: abp.localization.currentCulture.name }
                                ).toLocaleString();

                            },
                            

                        }


                    ]
                }

            )


        );

        createModal.onResult(function () {

            dataTable.ajax.reload();

        });

        $("#NewBookButton").click(function (e) {
            e.preventDefault(); 
            createModal.open();
        });

        editModal.onResult(function () {
            dataTable.ajax.reload();
        });

        
    }


);