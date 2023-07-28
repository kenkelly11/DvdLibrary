// DVD Library

$(document).ready(function () {
    $('#addForm').hide();
    loadDvds();



// Button Onclick calls



$('#searchButton').click(function (event) {
    $('#DvdTableDiv').hide();
    loadSearchOptions();
});

$('#cancelDeleteButton').click(function (event) {
    $('#deleteConfirmation').hide();
    $('#DvdTableDiv').show();
});

// Add and Cancel Add buttons

$('#addButton').click(function (event) {
    $('#addForm').show(); // shows the Add Form, doesn't actually Create a new Dvd
    $('#DvdTableDiv').hide();
});

$('#cancelAddButton').click(function (event) {
    hideAddForm();
});

// Create a Dvd onclick handler
$('#createDvdButton').click(function (event) {
		
    $.ajax({
        type: 'POST',
        url: 'http://localhost:63143/dvd', // connects to the VS project by its URL
        data: JSON.stringify({ // send Dvd data as a string to the VS server 
		// returns the value attribute
            title: $('#addTitle').val(),
            director: $('#addDirector').val(),
            releaseYear: $('#addReleaseDate').val(),
            rating: $('#addRating').val(),
            notes: $('#addNotes').val()
        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function () { // if successful, save the data
			
			// clear error messages
            $('#errorMessages').empty();
			
		// sets the value attribute
            $('#addTitle').val('');
            $('#addDirector').val('');
            $('#addReleaseDate').val('');
            $('#addRating').val('');
            $('#addNotes').val('');
			hideAddForm();
            loadDvds();
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  You should probably fill in every one of those Add fields.'));
        }
    });
});

// Edit/Update Button onclick handler
$('#updateEditButton').click(function (event) {
		
    $.ajax({
        type: 'PUT', // finds HTTP method in VS controller that accepts Put, and has id parameter
        url: 'http://localhost:63143/dvd/' + $('#editDvdId').val(),
        data: JSON.stringify({ // sends Dvd data to the VS server
            dvdId: $('#editDvdId').val(),
            title: $('#editTitle').val(),
            director: $('#editDirector').val(),
            releaseYear: $('#editReleaseDate').val(),
            rating: $('#editRating').val(),
            notes: $('#editNotes').val()
        }),
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        'dataType': 'json',
        success: function () {
			
            $('#errorMessages').empty();
            hideEditForm();
            loadDvds();
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  You should probably fill in every one of those Edit fields.'));
        }
    })
});

});



// load Dvd and Searh Options functions



function loadDvds() {
    clearDvdTable();
    var contentRows = $('#contentRows');

    $.ajax({
        type: 'GET', // finds HTTP method in VS controller that accepts Get
        url: 'http://localhost:63143/dvds',
        success: function (data) {
            // the jQuery .each function runs a for each loop through the dvds dataset
            $.each(data, function (index, Dvd) {
				// saves the dvd properties to a new variable
                var title = Dvd.Title;
                var releaseYear = Dvd.ReleaseYear;
                var director = Dvd.Director;
                var rating = Dvd.Rating;
                var notes = Dvd.Notes;
                var id = Dvd.DvdId;

                var row = '<tr>';
                row += '<td>' + title + '</td>';
                row += '<td>' + releaseYear + '</td>';
                row += '<td>' + director + '</td>';
                row += '<td>' + rating + '</td>';

                row += '<td><button onclick="showEditForm(' + id + ')">Edit</a>' + '<button onclick="showDeleteConfirmation(' + id + ')">Delete</a></td>';

                row += '</tr>';

                contentRows.append(row); // add each row to contentRows, in the Dvd table
				//hideAddForm();
            })
        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service. Cannot load Dvds.'));
        }
    });
}

function loadSearchOptions(){
clearDvdTable();

var contentRows = $('#contentRows');
var select = $("#searchCategory option:selected").text();
var search = $("#searchInput").val();

	$.ajax({
		type: 'GET', // finds HTTP method in VS controller that accepts Get, and has "select" and "search" parameters
		url: 'http://localhost:63143/dvds/' + select + '/' + search,
		success: function (data, status) {
			// the .each function runs a for each loop through the Dvds dataset
			$.each(data, function (index, dvd) {
				// saves the dvd properties to a new variable
				var title = dvd.Title;
				var date = dvd.ReleaseYear;
				var director = dvd.Director;
				var rating = dvd.Rating;
				var notes = dvd.Notes;
				var id = dvd.DvdId;


				var row = '<tr>';
				row += '<td>' + title + '</td>';
				row += '<td>' + date + '</td>';
				row += '<td>' + director + '</td>';
				row += '<td>' + rating + '</td>';


				row += '<td><button onclick="showEditForm(' + id + ')">Edit</button> ' + '<button onclick="showDeleteConfirmation( ' + id + ')">Delete</button></td>';

				row += '</tr>';
				contentRows.append(row);
				hideAddForm();
			});
		},
		error: function () {
			$('#errorMessages')
				.append($('<li>')
					.attr({ class: 'list-group-item list-group-item-danger' })
					.text('Error calling web service. Could not find an associated Dvd.'));
		}
	});
}



// Hide and Show form functions



function hideEditForm() {

    $('#errorMessages').empty();

    $('#editTitle').val('');
    $('#editDirector').val('');
    $('#editReleaseDate').val('');
    $('#editRating').val('');
    $('#editNotes').val('');
    $('#editFormDiv').hide();
    $('#DvdTableDiv').show();
}

function hideAddForm() {

    $('#errorMessages').empty();

    $('#editTitle').val('');
    $('#editDirector').val('');
    $('#editReleaseDate').val('');
    $('#editRating').val('');
    $('#editNotes').val('');
    $('#addForm').hide();
    $('#DvdTableDiv').show();
}

function showEditForm(dvdId) {
    // clear errorMessages
    $('#errorMessages').empty();
    // get dvd details from the server
    $.ajax({
        type: 'GET',
        url: 'http://localhost:63143/dvd/' + dvdId,
        success: function (data, status) { // fill and show the form on success
            $('#editDvdId').val(dvdId);
            $('#editTitle').val(data.Title);
            $('#editDirector').val(data.Director);
            $('#editReleaseDate').val(data.ReleaseYear);
            $('#editRating').val(data.Rating);
            $('#editNotes').val(data.Notes);

        },
        error: function () {
            $('#errorMessages')
                .append($('<li>')
                    .attr({ class: 'list-group-item list-group-item-danger' })
                    .text('Error calling web service.  Please try again later.'));
        }
    });
    $('#DvdTableDiv').hide();
    $('#editFormDiv').show();
}

// called when Delete button is clicked
function showDeleteConfirmation(dvdId) {
    $('#DvdTableDiv').hide();
    $('#deleteConfirmation').show();

    $('#deleteConfirmationButton').click(function (event) {
        deleteDVD(dvdId);
        $('#deleteConfirmation').hide();
        $('#DvdTableDiv').show();
    });
}



// Delete and Clear



function deleteDVD(dvdId) {
    $.ajax({
        type: 'DELETE', // uses HTTP method in VS controller that accepts Delete
        url: 'http://localhost:63143/dvd/' + dvdId,
        success: function (status) {
            loadDvds();
        }
    });
}

function clearDvdTable() {
    $('#contentRows').empty();
}
