function addQuestion(form) {
    

}
function addAnswer() {
    var hiddenInput = $('.multipleChoiceAnswers').first().find('input[type=hidden]');
    var numAnswers = parseInt(hiddenInput.val());
    numAnswers = numAnswers + 1;
    $('#addQuestionForm .multipleChoiceAnswers .multipleChoiceAnswer').last().next().after('<input type="text" class="multipleChoiceAnswer" name="answer' + numAnswers + '" placeholder="Answer #' + numAnswers + '" /><br />');
    $('#addQuestionForm #correctAnswer').append('<option value="' + numAnswers + '">' + numAnswers + "</option>");
    hiddenInput.attr('value', numAnswers);
}
function removeAnswer() {
    var hiddenInput = $('.multipleChoiceAnswers').first().find('input[type=hidden]');
    var numAnswers = hiddenInput.val();
    hiddenInput.attr('value', numAnswers - 1);
    var toRemove = $('#addQuestionForm .multipleChoiceAnswer').last();
    toRemove.next().remove();
    toRemove.remove();
    $('#addQuestionForm #correctAnswer').children().last().remove();
}
$(document).ready(function () {
    $('#multipleChoice').change(function (event) {
        if (event.target.checked) {
            $('#addQuestionForm').find('#textAnswer').remove();
            var toAppend = $('#copyMe').clone().children();
            toAppend.appendTo('#addQuestionForm');
        }
        else {
            $('#addQuestionForm').find('.multipleChoiceAnswers').remove();
            $('#addQuestionForm').append('<input type="text" name="textAnswer" id="textAnswer" placeholder="Correct Answer" />');
        }
    });
    $('#addQuestionForm').submit(function (event) {
        event.preventDefault();
        var form = $(this).children('input').clone();
        var multipleChoiceAnswers = $(this).children('.multipleChoiceAnswers').clone();
        if (multipleChoiceAnswers.length > 0) {
            var correctAnswer = multipleChoiceAnswers.children('#correctAnswer').clone();
            var answerList = multipleChoiceAnswers.children('.multipleChoiceAnswer').clone();
            correctAnswer.appendTo(form);
            answerList.appendTo(form);
        }
        $.post("/Game/AddQuestion", form.serialize());
    })
});