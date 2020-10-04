export const formatDate = (input) => {
  const dateParse = new Date(input);
  const output = dateParse.toLocaleString().split(", ")[0];
  return output;
};
