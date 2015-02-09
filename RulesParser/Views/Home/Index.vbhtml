@ModelType Rule
@Code
    ViewData("Title") = "Home Page"
End Code

@Section scripts 
    <!--Script references. -->
    <script src="~/Scripts/jquery.signalR-2.1.2.min.js"></script>
    <script src="~/signalr/hubs"></script>
    <!--The jQuery library is required and is referenced by default in _Layout.cshtml. -->
    <!--Reference the SignalR library. -->
    <!--SignalR script to update the chat page and send messages.-->
    <script>
        $(function () {
            // Reference the auto-generated proxy for the hub.
            var chat = $.connection.chatHub;
            // Create a function that the hub can call back to display messages.
            chat.client.addNewMessageToPage = function (name, message) {
                // Add the message to the page.
                $('#rules').append($('<li />').append($('<strong />').html(htmlEncode(name)))
                                              .append($('<span />').html(htmlEncode(message))));
            };
            // Start the connection.
            $.connection.hub.start().done(function () {
                $('#sendmessage').click(function () {
                    // Call the Send method on the hub.
                    chat.server.send($('#list').html(), $('#Pattern').val());
                });
            });
        });
        // This optional function html-encodes messages for display in the page.
        function htmlEncode(value) {
            var encodedValue = $("<div />").text(value).html();
            return encodedValue;
        }
    </script>
    <script>
        function handleFileSelect(evt) {
            evt.stopPropagation();
            evt.preventDefault();

            var files = evt.dataTransfer.files; // FileList object.

            // files is a FileList of File objects. List some properties.
            var output = [];
            for (var i = 0, f; f = files[i]; i++) {
                output.push('<li><strong>', escape(f.name), '</strong> (', f.type || 'n/a', ') - ',
                            f.size, ' bytes, last modified: ',
                            f.lastModifiedDate ? f.lastModifiedDate.toLocaleDateString() : 'n/a',
                            '</li>');
                htmlEncode("")
                //var reader = new FileReader();

                //// Closure to capture the file information.
                //reader.onload = (function (theFile) {
                //    return function (e) {
                //        // Render thumbnail.
                //        var span = document.createElement('span');
                //        span.innerHTML = ['<img class="thumb" src="', e.target.result,
                //                          '" title="', escape(theFile.name), '"/>'].join('');
                //        document.getElementById('list').insertBefore(span, null);
                //    };
                //})(f);

                //// Read in the image file as a data URL.
                //reader.readAsDataURL(f);
            }
            document.getElementById('list').innerHTML = '<ul>' + output.join('') + '</ul>';
        }

        function handleDragOver(evt) {
            evt.stopPropagation();
            evt.preventDefault();
            evt.dataTransfer.dropEffect = 'copy'; // Explicitly show this is a copy.
        }

        var dropZone = document.getElementById('drop_zone');
        dropZone.addEventListener('dragover', handleDragOver, false);
        dropZone.addEventListener('drop', handleFileSelect, false);
    </script>
End Section
@Using Html.BeginForm()
    @<div id="drop_zone">Drop files here</div>
    @<div id="list"></div>
    @<input type="text" multiple="multiple" id="Pattern" value="<h2>"  />
    @<input type="button" id="sendmessage" value="Parse" />
End Using
<ul id="rules"></ul>