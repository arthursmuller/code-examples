import { FC, useEffect, useState } from 'react';

import { useLocation } from 'react-router-dom';
import { ExpandedIndex } from '@chakra-ui/react';

import { PageLayout, FaqList } from '@pcf/design-system';
import { useSubRouteMenu } from 'features/main/components/use-sub-route-menu';
import { useNavigatePathUp } from 'hooks';

import { faqQuestions } from './ajuda-questions';

export const Ajuda: FC = () => {
  useSubRouteMenu('Ajuda');
  const navigateUp = useNavigatePathUp();
  const location = useLocation<{ questionId?: string }>();
  const [expandedIndex, setexpandedIndex] = useState<ExpandedIndex>([]);

  const questionId = location?.state?.questionId;

  useEffect(() => {
    if (questionId) {
      setTimeout(() => {
        const element = document.querySelector(
          `[data-index="${Number(questionId)}"]`,
        );

        setexpandedIndex((old) =>
          Array.isArray(old)
            ? old.concat(Number(questionId))
            : [Number(questionId)],
        );

        if (element) {
          element.scrollIntoView({ block: 'start', behavior: 'smooth' });
        }
      }, 400);
    }
  }, [questionId]);

  return (
    <PageLayout>
      <PageLayout.Header>
        <PageLayout.BackButton onClick={navigateUp} />
        <PageLayout.Title>Perguntas frequentes</PageLayout.Title>
      </PageLayout.Header>

      <PageLayout.Content>
        <FaqList
          index={expandedIndex}
          onChange={setexpandedIndex}
          questions={faqQuestions}
        />
      </PageLayout.Content>
    </PageLayout>
  );
};
