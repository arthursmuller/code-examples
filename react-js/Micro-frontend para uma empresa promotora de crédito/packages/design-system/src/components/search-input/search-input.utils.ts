export const highligthWord = (text: string, word: string): string => {
  return text.replace(
    new RegExp(word, 'gi'),
    (match) => `<span class="highlight-keyword">${match}</span>`,
  );
};
