$(document).ready(function () {

    $('.modal').modal();

    $('.show-info').on('click', showInfoHendler);

    $('.remove-user').on('click', removeUser);

    let phoneCounter = 0;

    $('#add-phone').on('click', addPhone);

    $('#create-user-btn').on('click', createUserBtn);


    function showInfoHendler(e) {
        e.preventDefault();

        let userId = $(this).data('userid');

        console.log(this);

        $.ajax({
            url: "home/GetUser?id=" + userId,
            type: "GET"
        }).done(function (response) {
            let elem = $('#modal1');

            elem.html(response);

            let instance = M.Modal.getInstance(elem);
            instance.open();
        });
    }

    function removeUser(e) {
        e.preventDefault();

        let userId = $(this).data('userid');

        $.ajax({
            url: "home/DeleteUser?id=" + userId,
            type: "DELETE"
        }).done(function (response) {
            
            $(`.item-number-${userId}`).remove();
        });
    }

    function addPhone() {
        let elem = $('#phones-list');

        let currId = phoneCounter + 1;

        let removeId = `remove-phone-${currId}`;

        let newBlock = `<div class="row s12" id="phone-block-${currId}">
                            <div class="input-field col s10">
                                <input id="phone${currId}" type="tel" name="phones[][number]">
                                <label for="phone${currId}">Phone</label>
                            </div>
                            <a data-phoneid="${currId}" id="${removeId}" class=" waves-effect waves-light btn col s2">Remove Phone</a>
                        </div>`;
        
        elem.append(newBlock);

        $('#' + removeId).on('click', removePhone);

        phoneCounter++;

    }

    function removePhone() {
        let parent = $(this).parent();

        $(parent).remove();

        phoneCounter--;
    }

    function createUserBtn(e) {

        e.preventDefault();

        console.log(JSON.stringify($('#user-create').serializeJSON()));

        $.post('home/CreateUser', $('#user-create').serializeJSON()).done(function (response) {

            $('#collection-users').append(response);

        }).done(function () {

            $('.show-info').off('click');

            $('.remove-user').off('click'); 

            $('.show-info').on('click', showInfoHendler);

            $('.remove-user').on('click', removeUser);

            let instance = M.Modal.getInstance($('#modal2'));
            instance.close();

            $('#user-create').trigger("reset");
        });

    }

});