var result = { message: 'success', keyvalues: [{ key: 'Result', value: undefined }] }
var args = process.argv.slice(2);

if (args.length === 3 && (args[1] === '+' || args[1] === '-' || args[1] === '*' || args[1] === '/')) {
    // We got correct args

    if (args[1] === '+') {
        result.keyvalues[0].value = (parseInt(args[0]) + parseInt(args[2])).toString();
    }
}
console.log(JSON.stringify(result));