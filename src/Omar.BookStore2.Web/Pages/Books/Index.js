

$(

    function () {

        const l = abp.localization.getResource('BookStore2');
        const createModal = new abp.ModalManager(abp.appPath + "Books/CreateModal");

        const editModal = new abp.ModalManager(abp.appPath + "Books/EditModal");

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
                            data:"authorName"
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