import { render } from '@testing-library/react';

import { Ajuda, faqQuestions } from './ajuda';

test('faq questions renders', async () => {
  const { getByText } = render(<Ajuda />);

  faqQuestions.map((question) =>
    expect(getByText(question.question)).toBeInTheDocument(),
  );
});
